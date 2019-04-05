#include "all.h"

void ProcMain(void)
{
#if !LOG_ENABLED
	Test0001();
#elif 1 // test
	Test0001();
#elif 1 // test
	Test0001();
#else // test
	Test0001();
#endif
}
