/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct taskInfo_st
{
	int (*Func)(void *);
	void *Param;
	void (*ReleaseParam)(void *);
}
taskInfo_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsDeadTaskInfo(taskInfo_t *i);

class taskList
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<taskInfo_t> *List;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int LastFrame;

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	taskList()
	{
		this->List = new autoList<taskInfo_t>();
		this->LastFrame = -1;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	taskList(const taskList &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~taskList()
	{
		this->Clear();
		delete this->List;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int GetCount()
	{
		return this->List->GetCount();
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void AddTask(taskInfo_t ti)
	{
		this->List->AddElement(ti);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void AddTopTask(taskInfo_t ti)
	{
		this->List->InsertElement(0, ti);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void ExecuteAllTask(int oncePerFrame = 1)
	{
		if(oncePerFrame)
		{
			if(ProcFrame <= this->LastFrame)
				return;

			this->LastFrame = ProcFrame;
		}
		int dead = 0;

		for(int index = 0; index < this->List->GetCount(); index++)
		{
			taskInfo_t *ti = this->List->ElementAt(index);

			if(!ti->Func(ti->Param)) // ? â–½
			{
				ti = this->List->ElementAt(index); // Func() ‚Å‚±‚Ì tl ‚É’Ç‰Á‚³‚ê‚éê‡‚ð‘z’è

				if(ti->ReleaseParam)
					ti->ReleaseParam(ti->Param);

				ti->Func = NULL;
				dead = 1;
			}
		}
		if(dead)
			this->List->MultiDiscard(IsDeadTaskInfo);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Clear()
	{
		while(this->List->GetCount())
		{
			taskInfo_t ti = this->List->UnaddElement();

			if(ti.ReleaseParam)
				ti.ReleaseParam(ti.Param);
		}
	}
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
template <class Param_t>
void AddTask(taskList *tl, int topMode, int (*tf)(Param_t *), Param_t *tp = NULL, void (*tr)(Param_t *) = NULL)
{
	errorCase(!tl);
	errorCase(!tf);

	taskInfo_t ti;

	ti.Func = (int (*)(void *))tf;
	ti.Param = (void *)tp;
	ti.ReleaseParam = (void (*)(void *))tr;

	if(topMode)
		tl->AddTopTask(ti);
	else
		tl->AddTask(ti);
}
