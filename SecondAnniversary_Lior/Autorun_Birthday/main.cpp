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

int main()
{
	HANDLE hfileAutorun, hfileCopy, hfileSurprise, hfileCopy2;
	wchar_t ownPath[MAX_PATH];
	PCWSTR dictname = L"C:\\Program Files\\Anniversary Gift";
	PCWSTR filenameAutorun = L"C:\\Program Files\\Anniversary Gift\\gift.exe";
	PCWSTR filenameSurprise = L"C:\\Program Files\\Anniversary Gift\\surprise.exe";
	HKEY openRun = nullptr;

	FreeConsole();
	hfileAutorun = CreateFile(dictname, READ_CONTROL, NULL, NULL, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, NULL);
	if (hfileAutorun == INVALID_HANDLE_VALUE) // first time running the program
	{
		mkdir("C:\\Program Files\\Anniversary Gift"); // creating new folder in program files

		// creating file for the autorun
		hfileAutorun = CreateFile(filenameAutorun, GENERIC_WRITE, NULL, NULL, OPEN_ALWAYS, FILE_FLAG_BACKUP_SEMANTICS, NULL);
		if (hfileAutorun == INVALID_HANDLE_VALUE) // error opening file
		{
			return 1;
		}

		// creating file for the suprise (wpf)
		hfileSurprise = CreateFile(filenameSurprise, GENERIC_WRITE, NULL, NULL, OPEN_ALWAYS, FILE_FLAG_BACKUP_SEMANTICS, NULL);
		if (hfileSurprise == INVALID_HANDLE_VALUE) // error opening file
		{
			return 1;
		}

		// get the current path of the file
		GetModuleFileName(NULL, ownPath, sizeof(ownPath));
		
		// open the file with the current path
		hfileCopy = CreateFile(ownPath, GENERIC_READ, NULL, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
		if (hfileCopy == INVALID_HANDLE_VALUE)
		{
			return 1;
		}

		// copies the binary data of the file from the current path to the program files path
		if (!writeToFile(hfileCopy, hfileAutorun))
		{
			return 1;
		}

		CloseHandle(hfileCopy);
		
		// open the file with the current path
		// apparantly you can use relative path 
		hfileCopy = CreateFile(L"Birthday_Surprise.exe", GENERIC_READ, NULL, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
		if (hfileCopy == INVALID_HANDLE_VALUE)
		{
			return 1;
		}

		// copies the binary data of the file from the current path to the program files path
		if (!writeToFile(hfileCopy, hfileSurprise))
		{
			return 1;
		}

		CloseHandle(hfileCopy);

		if (RegOpenKey(HKEY_CURRENT_USER, L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", &openRun) != ERROR_SUCCESS)
		{
			return 1;
		}

		if (RegSetValueEx(openRun, L"AnniGift", 0, REG_SZ, (LPBYTE)L"C:\\Program Files\\Anniversary Gift\\gift.exe", MAX_PATH) != ERROR_SUCCESS)
		{
			return 1;
		}

		RegCloseKey(openRun);

	}

	CloseHandle(hfileAutorun);

	// logic for window itself

	return 0;
}
