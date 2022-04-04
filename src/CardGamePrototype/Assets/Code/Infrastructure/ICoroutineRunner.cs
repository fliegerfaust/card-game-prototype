﻿using System.Collections;
using UnityEngine;

namespace Code.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}