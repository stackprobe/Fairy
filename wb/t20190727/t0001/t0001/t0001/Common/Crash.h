/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double GetDistance(double x, double y);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double GetDistance(double x1, double y1, double x2, double y2);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MakeXYSpeed(double x, double y, double destX, double destY, double speed, double &speedX, double &speedY, double distanceMin = 1.0);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Circle(
	double x1, double y1, double rCir1,
	double x2, double y2, double rCir2
	);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Point(
	double x1, double y1, double rCir,
	double x2, double y2
	);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Rect(
	double x, double y, double rCir,
	double l, double t, double r, double b
	);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Rect_Point(
	double l, double t, double r, double b,
	double x, double y
	);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Rect_Rect(
	double l1, double t1, double r1, double b1,
	double l2, double t2, double r2, double b2
	);
