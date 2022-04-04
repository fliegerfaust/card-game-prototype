using System;
using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
  public interface IInputService
  {
    public event Action<Vector3> OnClick;
  }
}