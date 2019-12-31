using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fvc.exp
{

    public class FVCUiGameStateBase : MonoBehaviour
    {
        protected FVCGameState _State;

        public FVCGameState State
        {
            get { return _State; }
            set
            {
                if (_State == value)
                {
                    return;
                }
                _State = value;
                _OnGameStateChanged(value);
            }
        }

        public virtual void _OnGameStateChanged(FVCGameState state)
        {
        }
    }

}
