using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace AccuChekVRGame
{
    public class ShowKeyboard : MonoBehaviour
    {
        public bool IsPhoneInput;
        public TMP_InputField inputfield;
        // Start is called before the first frame update
        void Start()
        {
            //inputfield = GetComponent<TMP_InputField>();
            inputfield.onSelect.AddListener(x => OpenKeyboard());
        }

        public void OpenKeyboard()
        {
            NonNativeKeyboard.Instance.InputField = inputfield;
            NonNativeKeyboard.Instance.InputField.text = string.Empty;
            NonNativeKeyboard.Instance.PresentKeyboard(inputfield.text);

            if (IsPhoneInput)
                NonNativeKeyboard.Instance.ActivateSpecificKeyboard(NonNativeKeyboard.LayoutType.Symbol);
        }
    }
}
