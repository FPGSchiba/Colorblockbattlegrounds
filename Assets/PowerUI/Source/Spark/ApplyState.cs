//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright © 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------namespace Css{		/// <summary>	/// Defines how a CSS property has been applied.	/// </summary>		public enum ApplyState{				/// <summary>Everything was fine.</summary>		Ok,		/// <summary>Reload the value from the ComputedStyle before passing the value on to child elements.		/// This allows CSS properties to store information within the CSS value itself.</summary>		ReloadValue			}	}