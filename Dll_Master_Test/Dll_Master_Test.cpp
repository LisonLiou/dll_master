// Dll_Master_Test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include <Windows.h>
#include "cdll/master.h"

typedef int __declspec(dllimport)(*FUNC_divide)(int arg);

int main()
{
	HINSTANCE hDll = LoadLibrary(L"cdll\\Dll_Master.dll");

	if (hDll == NULL) {
		std::cout << "dll load failed " << GetLastError();
	}
	else {
		FUNC_divide eval;
		eval = (FUNC_divide)GetProcAddress(hDll, "divide");
		int ret = eval(13);

		std::cout << "Hello World! " << ret << "\n";

		FreeLibrary(hDll);
	}
}



