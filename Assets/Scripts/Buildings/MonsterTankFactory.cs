﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTankFactory : Building
{
	protected override void Start()
	{
		base.Start();
	}

	protected override void ShowBuildingItems()
	{
		base.ShowBuildingItems();
	}

	protected override void setData()
	{
		numberOfItems = 20;
	}
}
