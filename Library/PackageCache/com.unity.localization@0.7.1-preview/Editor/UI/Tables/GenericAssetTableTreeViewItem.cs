using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Localization.Tables;

namespace UnityEditor.Localization.UI
{
    class GenericAssetTableTreeViewItem<T1> : TreeViewItem
    {
        public virtual SharedTableData.SharedTableEntry SharedEntry { get; set; }

        public string Key
        {
            get => SharedEntry.Key;
            set => SharedEntry.Key = value;
        }

        public uint KeyId => SharedEntry.Id;

        public bool Selected { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        /// <summary>
        /// Called during the setup of the tree view.
        /// </summary>
        public virtual void Initialize(LocalizedTableCollection collection, int startIdx) {}

        /// <summary>
        /// Called before the key entry is deleted.
        /// </summary>
        public virtual void OnDeleteKey() {}
    }
}
