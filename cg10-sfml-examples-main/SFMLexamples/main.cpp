#include "lighting_file.h"
#include <iostream>

using namespace std;

int main()
{
	auto t = lighting_file();
	t.init("model.obj");
	t.Paint();
}