using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
namespace Bunny_TK.UI
{
    public class SimpleAnimatedUI : BaseAnimatedUI
    {
        [SerializeField] protected string showingStateName;
        [SerializeField] protected string hiddenStateName;

        [SerializeField] protected AnimationParameterType animationParameterType = AnimationParameterType.Trigger;
        [SerializeField] protected string parameterName;

        [SerializeField] protected bool parameterBoolInverted = false; //if TRUE, will SetBool(false) to Show and will SetBool(true) to hide.

        public override bool IsInTransition
        {
            get
            {
                if (animator == null) return false;

                return animator.IsInTransition(0);
            }
        }
        public override bool IsVisible
        {
            get
            {
                if (animator == null) return false;
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (stateInfo.IsName(showingStateName)) return true;
                if (stateInfo.IsName(hiddenStateName)) return false;
                return false;
            }
            set
            {
                if (animator == null) return;
                if (IsInTransition) return;

                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                //In case the bool parameter of the animator is inverted (true == hidden)
                bool targetStateIsVisible = parameterBoolInverted ? !value : value;

                //Update parameter only if neccessary
                if (IsVisible != value)
                    UpdateAnimator(parameterName, targetStateIsVisible);
            }
        }

        private void UpdateAnimator(string parameterName, bool value = false)
        {
            switch (animationParameterType)
            {
                case AnimationParameterType.Trigger:
                    animator.SetTrigger(parameterName);
                    break;
                case AnimationParameterType.Bool:
                    animator.SetBool(parameterName, value);
                    break;
                default:
                    break;
            }
        }

        public enum AnimationParameterType
        {
            Trigger = 0,
            Bool = 1
        }
    }
}
