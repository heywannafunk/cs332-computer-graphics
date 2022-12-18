// Рисует треугольник, цвет которого можно менять с клавиатуры
using namespace std;
#include <iostream>

#include "lighting_file.h"
#include "figure_model.h"
#include "lighting_Toon.h"
#include "lighting_Minnaert.h"

int main()
{
	auto t =lighting_file();
	t.init("model.obj");
	t.Paint();
	
	auto toon = lighting_Toon();
	toon.init("model.obj");
	toon.Paint();
	
	auto minnaert = lighting_minnaert();
	minnaert.init("model.obj");
	minnaert.Paint();
	
}