#include <Windows.h>
#include <iostream>
#include <direct.h>
#include <string>
#pragma warning(disable : 4996)

using std::string; using std::wstring;

bool writeToFile(HANDLE readFile, HANDLE writeFile)
{
	DWORD filesize = GetFileSize(readFile, NULL);
	CHAR* pbuffer = new CHAR[filesize];
	if (!ReadFile(readFile, pbuffer, filesize, NULL, NULL))
	{
		return false;
	}
	if (!WriteFile(writeFile, pbuffer, filesize, NULL, NULL))
	{
		return false;
	}
	delete[] pbuffer;
	return true;
}

bool copyFile(LPCWSTR sourceFilePath, LPCWSTR destFilePath)
{
	HANDLE hFileDest, hFileSource;

	// creating file at the destination path
	hFileDest = CreateFile(destFilePath, GENERIC_WRITE, NULL, NULL, OPEN_ALWAYS, FILE_FLAG_BACKUP_SEMANTICS, NULL);
	if (hFileDest == INVALID_HANDLE_VALUE) // error opening file
	{
		return false;
	}

	// opening source file
	hFileSource = CreateFile(sourceFilePath, GENERIC_READ, NULL, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFileSource == INVALID_HANDLE_VALUE) // error opening file
	{
		CloseHandle(hFileDest);
		return false;
	}

	// copies the binary data of the file from the current path to the program files path
	if (!writeToFile(hFileSource, hFileDest))
	{
		CloseHandle(hFileDest);
		CloseHandle(hFileSource);
		return false;
	}

	CloseHandle(hFileDest);
	CloseHandle(hFileSource);
	return true;
}

int main()
{
	HANDLE hfileAutorun;
	wchar_t ownPath[MAX_PATH];
	PCWSTR dictname = L"C:\\Program Files\\Anniversary Gift";
	PCWSTR filenameSurprise = L"C:\\Program Files\\Anniversary Gift\\surprise.exe";
	HKEY openRun = nullptr;

	PCWSTR sources[] =
	{
		L"Birthday_Surprise.exe",
		L"confetti.gif",
		L"confettisound.mp3",
		L"AudioSwitcher.AudioApi.CoreAudio.dll",
		L"AudioSwitcher.AudioApi.dll",
		L"WpfAnimatedGif.dll",
		L"WpfAnimatedGif.pdb",
		L"WpfAnimatedGif.xml"
	};

	PCWSTR destinations[] =
	{
		L"C:\\Program Files\\Anniversary Gift\\surprise.exe",
		L"C:\\Program Files\\Anniversary Gift\\confetti.gif",
		L"C:\\Program Files\\Anniversary Gift\\confettisound.mp3",
		L"C:\\Program Files\\Anniversary Gift\\AudioSwitcher.AudioApi.CoreAudio.dll",
		L"C:\\Program Files\\Anniversary Gift\\AudioSwitcher.AudioApi.dll",
		L"C:\\Program Files\\Anniversary Gift\\WpfAnimatedGif.dll",
		L"C:\\Program Files\\Anniversary Gift\\WpfAnimatedGif.pdb",
		L"C:\\Program Files\\Anniversary Gift\\WpfAnimatedGif.xml"
	};

	FreeConsole();
	hfileAutorun = CreateFile(dictname, READ_CONTROL, NULL, NULL, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, NULL);
	if (hfileAutorun == INVALID_HANDLE_VALUE) // first time running the program
	{
		mkdir("C:\\Program Files\\Anniversary Gift"); // creating new folder in program files

		for (int i = 0; i < 8; i++)
		{
			if (!copyFile(sources[i], destinations[i]))
			{
				return 1;
			}
		}

		// open regedit autorun
		if (RegOpenKey(HKEY_CURRENT_USER, L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", &openRun) != ERROR_SUCCESS)
		{
			return 1;
		}

		// writes to regedit autorun
		if (RegSetValueEx(openRun, L"AnniGift", 0, REG_SZ, (LPBYTE)L"C:\\Program Files\\Anniversary Gift\\surprise.exe", MAX_PATH) != ERROR_SUCCESS)
		{
			return 1;
		}

		RegCloseKey(openRun);

	}

	CloseHandle(hfileAutorun);

	return 0;
}
