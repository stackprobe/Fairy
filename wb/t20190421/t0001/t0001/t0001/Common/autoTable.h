/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
template <class Element_t>
class autoTable
{
private:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoList<autoList<Element_t> *> *Rows;
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t (*CreateCellFunc)(void);
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void (*ReleaseCellFunc)(Element_t);

public:
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoTable(Element_t (*createCellFunc)(void), void (*releaseCellFunc)(Element_t), int w = 1, int h = 1)
	{
		errorCase(!createCellFunc);
		errorCase(!releaseCellFunc);

		this->Rows = new autoList<autoList<Element_t> *>();
		this->CreateCellFunc = createCellFunc;
		this->ReleaseCellFunc = releaseCellFunc;
		this->Resize(w, h);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	autoTable(const autoTable &source)
	{
		error();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	~autoTable()
	{
		while(this->Rows->GetCount())
		{
			autoList<Element_t> *row = this->Rows->UnaddElement();

			while(row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			delete row;
		}
		delete this->Rows;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void SetCallBack(Element_t (*createCellFunc)(void), void (*releaseCellFunc)(Element_t) = NULL)
	{
		if(createCellFunc)
			this->CreateCellFunc = createCellFunc;

		if(releaseCellFunc)
			this->ReleaseCellFunc = releaseCellFunc;
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Change(autoTable<Element_t> *otherTable, int withCallBack = 0)
	{
		m_swap(this->Rows, otherTable->Rows, void *);

		if(withCallBack)
		{
			m_swap(this->CreateCellFunc, otherTable->CreateCellFunc, void *);
			m_swap(this->ReleaseCellFunc, otherTable->ReleaseCellFunc, void *);
		}
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Resize(int w, int h)
	{
		errorCase(w < 1 || IMAX < w);
		errorCase(h < 1 || IMAX / w < h);

		while(h < this->Rows->GetCount())
		{
			autoList<Element_t> *row = this->Rows->UnaddElement();

			while(row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			delete row;
		}
		while(this->Rows->GetCount() < h)
		{
			this->Rows->AddElement(new autoList<Element_t>());
		}
		for(int rowidx = 0; rowidx < h; rowidx++)
		{
			autoList<Element_t> *row = this->Rows->GetElement(rowidx);

			while(w < row->GetCount())
			{
				this->ReleaseCellFunc(row->UnaddElement());
			}
			while(row->GetCount() < w)
			{
				row->AddElement(this->CreateCellFunc());
			}
		}
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int GetWidth()
	{
		return this->Rows->GetElement(0)->GetCount();
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	int GetHeight()
	{
		return this->Rows->GetCount();
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t *CellAt(int x, int y)
	{
		errorCase(x < 0 || this->GetWidth() <= x);
		errorCase(y < 0 || this->GetHeight() <= y);

		return this->Rows->GetElement(y)->ElementAt(x);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t GetCell(int x, int y)
	{
		return *this->CellAt(x, y);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void SetCell(int x, int y, Element_t e)
	{
		*this->CellAt(x, y) = e;
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void SetSmallestSize(int w, int h)
	{
		m_maxim(w, this->GetWidth());
		m_maxim(h, this->GetHeight());

		this->Resize(w, h);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t *RefCellAt(int x, int y)
	{
		this->SetMinimal(x + 1, y + 1);
		return this->CellAt(x, y);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t RefCell(int x, int y, Element_t dummyElement)
	{
		if(
			x < 0 || this->GetWidth() <= x ||
			y < 0 || this->GetHeight() <= y
			)
		{
			return dummyElement;
		}
		return this->GetCell(x, y);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	Element_t RefCellEx(int x, int y)
	{
		this->SetSmallestSize(x + 1, y + 1);
		return this->GetCell(x, y);
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void PutCell(int x, int y, Element_t e)
	{
		this->SetSmallestSize(x + 1, y + 1);
		return this->SetCell(x, y, e);
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Reset(int x, int y)
	{
		this->ReleaseCellFunc(this->GetCell(x, y));
		this->SetCell(x, y, this->CreateCellFunc());
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Reset(int l, int t, int w, int h)
	{
		for(int x = 0; x < w; x++)
		for(int y = 0; y < h; y++)
		{
			this->Reset(l + x, t + y);
		}
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Reset()
	{
		this->Reset(0, 0, this->GetWidth(), this->GetHeight());
	}
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	void Clear(int w, int h)
	{
		this->Resize(w, h);
		this->Reset(
			0,
			0,
			m_min(w, this->GetWidth()),
			m_min(h, this->GetHeight())
			);
	}

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	/*
		ã‰º”½“]
	*/
	void Reverse()
	{
		this->Rows->Reverse();
	}
};
