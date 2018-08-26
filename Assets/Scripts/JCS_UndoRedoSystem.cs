/**
 * $File: JCS_UndoRedoSystem.cs $
 * $Date: 2018-08-25 21:26:05 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright © 2018 by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JCSUnity
{
    /// <summary>
    /// Undo Redo system manager.
    /// </summary>
    public class JCS_UndoRedoSystem
        : MonoBehaviour
    {

        /*******************************************/
        /*            Public Variables             */
        /*******************************************/

        /*******************************************/
        /*           Private Variables             */
        /*******************************************/

#if (UNITY_EDITOR)
        [Header("** Helper Variables (JCS_UndoRedoSystem) **")]

        [Tooltip("Test this component with key?")]
        [SerializeField]
        private bool mTestWithKey = false;

        [Tooltip("Undo key.")]
        [SerializeField]
        private KeyCode mUndoKey = KeyCode.Z;

        [Tooltip("Redo key.")]
        [SerializeField]
        private KeyCode mRedoKey = KeyCode.Y;
#endif


        [Header("** Check Variables (JCS_UndoRedoSystem) **")]

        [Tooltip("All of the undo redo component this system handles.")]
        [SerializeField]
        private List<JCS_UndoRedoComponent> mAllUndoRedoComp = new List<JCS_UndoRedoComponent>();

        [Tooltip("List of next undo component.")]
        [SerializeField]
        private List<JCS_UndoRedoComponent> mUndoComp = new List<JCS_UndoRedoComponent>();

        [Tooltip("List of next redo component.")]
        [SerializeField]
        private List<JCS_UndoRedoComponent> mRedoComp = new List<JCS_UndoRedoComponent>();

        /*******************************************/
        /*           Protected Variables           */
        /*******************************************/

        /*******************************************/
        /*             setter / getter             */
        /*******************************************/

        /*******************************************/
        /*            Unity's function             */
        /*******************************************/

#if (UNITY_EDITOR)
        private void Update()
        {
            Test();
        }

        private void Test()
        {
            if (!mTestWithKey)
                return;

            if (JCS_Input.GetKeyDown(mUndoKey))
                UndoComponent();

            if (JCS_Input.GetKeyDown(mRedoKey))
                RedoComponent();
        }
#endif

        /*******************************************/
        /*              Self-Define                */
        /*******************************************/
        //----------------------
        // Public Functions

        /// <summary>
        ///  Undo next component.
        /// </summary>
        public void UndoComponent()
        {
            JCS_UndoRedoComponent undoComp = JCS_Utility.ListPopBack(mUndoComp);

            if (undoComp == null)
                return;

            undoComp.Undo();

            mRedoComp.Add(undoComp);
        }

        /// <summary>
        /// Redo next component.
        /// </summary>
        public void RedoComponent()
        {
            JCS_UndoRedoComponent redoComp = JCS_Utility.ListPopBack(mRedoComp);

            if (redoComp == null)
                return;

            redoComp.Redo();

            mUndoComp.Add(redoComp);
        }

        /// <summary>
        /// Add a undo redo component to the system in order to 
        /// get manage.
        /// </summary>
        /// <param name="comp"></param>
        public void AddUndoRedoComponentToSystem(JCS_UndoRedoComponent comp)
        {
            mAllUndoRedoComp.Add(comp);
        }

        /// <summary>
        /// Add component to next undo component.
        /// </summary>
        /// <param name="undoComp"></param>
        public void AddUndoComponent(JCS_UndoRedoComponent undoComp)
        {
            mUndoComp.Add(undoComp);
        }

        /// <summary>
        /// Add component to next redo component.
        /// </summary>
        /// <param name="redoComp"></param>
        public void AddRedoComponent(JCS_UndoRedoComponent redoComp)
        {
            mRedoComp.Add(redoComp);
        }

        /// <summary>
        /// Clear all redo component queue.
        /// </summary>
        public void ClearRedoComp()
        {
            mRedoComp.Clear();

            foreach (JCS_UndoRedoComponent comp in mAllUndoRedoComp)
            {
                comp.ClearAllRedo();
            }
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}
