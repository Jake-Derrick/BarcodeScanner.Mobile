﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoogleVisionBarCodeScanner
{
    public class CameraView : View
    {
        public static BindableProperty OnDetectedCommandProperty = BindableProperty.Create(nameof(OnDetectedCommand)
            , typeof(ICommand
            ), typeof(CameraView)
            , null
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).OnDetectedCommand = (ICommand)newValue);
        public ICommand OnDetectedCommand
        {
            get => (ICommand)GetValue(OnDetectedCommandProperty);
            set => SetValue(OnDetectedCommandProperty, value);
        }


        public static BindableProperty VibrationOnDetectedProperty = BindableProperty.Create(nameof(VibrationOnDetected)
            , typeof(bool)
            , typeof(CameraView)
            , true
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).VibrationOnDetected= (bool)newValue);
        public bool VibrationOnDetected
        {
            get => (bool)GetValue(VibrationOnDetectedProperty);
            set => SetValue(VibrationOnDetectedProperty, value);
        }


        //public static BindableProperty DefaultTorchOnProperty = BindableProperty.Create(nameof(DefaultTorchOn), typeof(bool), typeof(CameraView), false, propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).TorchOn = (bool)newValue);
        //[Obsolete("Use TorchOn")]
        //public bool DefaultTorchOn
        //{
        //    get => (bool)GetValue(DefaultTorchOnProperty);
        //    set => SetValue(DefaultTorchOnProperty, value);
        //}

        //public static BindableProperty AutoStartScanningProperty = BindableProperty.Create(nameof(AutoStartScanning), typeof(bool), typeof(CameraView), true);
        //[Obsolete("Use IsScanning")]
        //public bool AutoStartScanning
        //{
        //    get => (bool)GetValue(AutoStartScanningProperty);
        //    set => SetValue(AutoStartScanningProperty, value);
        //}

        public static BindableProperty RequestedFPSProperty = BindableProperty.Create(nameof(RequestedFPS)
            , typeof(float?)
            , typeof(CameraView)
            , null
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).RequestedFPS = (float?)newValue);
        /// <summary>
        /// Only Android will be reflected this setting
        /// </summary>
        public float? RequestedFPS
        {
            get => (float?)GetValue(RequestedFPSProperty);
            set => SetValue(RequestedFPSProperty, value);
        }


        public static BindableProperty ScanIntervalProperty = BindableProperty.Create(nameof(ScanInterval)
            , typeof(int)
            , typeof(CameraView)
            , 500
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).ScanInterval = (int)newValue);
        /// <summary>
        /// Only iOS will be reflected this setting, Default is 500ms, minimum value is 100ms
        /// </summary>
        public int ScanInterval
        {
            get => (int)GetValue(ScanIntervalProperty);
            set => SetValue(ScanIntervalProperty, value);
        }

        public static BindableProperty IsScanningProperty = BindableProperty.Create(nameof(IsScanning)
            , typeof(bool)
            , typeof(CameraView)
            , true
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).IsScanning = (bool)newValue);
        /// <summary>
        /// Disables or enables scanning
        /// </summary>
        public bool IsScanning
        {
            get => (bool)GetValue(IsScanningProperty);
            set => SetValue(IsScanningProperty, value);
        }

        public static BindableProperty TorchOnProperty = BindableProperty.Create(nameof(TorchOn)
            , typeof(bool)
            , typeof(CameraView)
            , false
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: (bindable, value, newValue) => ((CameraView)bindable).TorchOn = (bool)newValue);
        /// <summary>
        /// Disables or enables torch
        /// </summary>
        public bool TorchOn
        {
            get => (bool)GetValue(TorchOnProperty);
            set => SetValue(TorchOnProperty, value);
        }

        public event EventHandler<OnDetectedEventArg> OnDetected;
        public void TriggerOnDetected(List<BarcodeResult> barCodeResults)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OnDetected?.Invoke(this, new OnDetectedEventArg { BarcodeResults = barCodeResults });
                OnDetectedCommand?.Execute( new OnDetectedEventArg { BarcodeResults = barCodeResults });
            });
        }
    }

    public class OnDetectedEventArg : EventArgs
    {
        public List<BarcodeResult> BarcodeResults { get; set; }
        public OnDetectedEventArg()
        {
            BarcodeResults = new List<BarcodeResult>();
        }
    }
}
