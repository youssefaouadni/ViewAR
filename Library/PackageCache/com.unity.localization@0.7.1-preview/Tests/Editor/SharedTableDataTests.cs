using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization.Tables;

namespace UnityEditor.Localization.Tests
{
    public class SharedTableDataTests
    {
        SharedTableData m_SharedTableData;

        [SetUp]
        public void Init()
        {
            m_SharedTableData = ScriptableObject.CreateInstance<SharedTableData>();

            for (int i = 0; i < 100; ++i)
            {
                m_SharedTableData.AddKey("My Key " + i);
            }
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(m_SharedTableData);
        }

        [TestCase("Unique Key 1")]
        [TestCase("Unique Key 2")]
        [TestCase("Unique Key 3")]
        [TestCase("{0} {1} ####--++></?")]
        public void AddAndVerifyKeyIsAdded(string keyName)
        {
            Assert.IsFalse(m_SharedTableData.Contains(keyName), "Expected the key to not already be in the shared table data.");
            Assert.IsNotNull(m_SharedTableData.AddKey(keyName), "Expected the key to added however it was not.");
            Assert.IsTrue(m_SharedTableData.Contains(keyName), "Expected the key to be contained in the  shared table data.");
        }

        uint GetKeyIdAndVerifyItIsValid(string keyName)
        {
            var keyId = m_SharedTableData.GetId(keyName);
            Assert.IsTrue(keyId != SharedTableData.EmptyId, "Failed to find the id of the added key");
            return keyId;
        }

        [Test]
        public void All_KeyIds_AreUnique()
        {
            HashSet<uint> usedKeys = new HashSet<uint>();
            foreach (var entry in m_SharedTableData.Entries)
            {
                Assert.IsFalse(usedKeys.Contains(entry.Id), "Expected all key ids to be unique, however this key has already been used: " + entry.Id);
                usedKeys.Add(entry.Id);
            }
        }

        [Test]
        public void All_Keys_AreUnique()
        {
            HashSet<string> usedKeys = new HashSet<string>();
            foreach (var entry in m_SharedTableData.Entries)
            {
                Assert.IsFalse(usedKeys.Contains(entry.Key), "Expected all keys to be unique, however this key has already been used: " + entry.Key);
                usedKeys.Add(entry.Key);
            }
        }

        [Test]
        public void AddKey_AddingDuplicateKey_ReturnsNull()
        {
            const string keyName = "Duplicate Key";
            AddAndVerifyKeyIsAdded(keyName);
            Assert.IsNull(m_SharedTableData.AddKey(keyName), "Expected the key to not be added a second time.");
        }

        [Test]
        public void AddKey_AllowsRemovedKeyToBeReAdded()
        {
            const string keyName = "Remove And Add Key";
            AddAndVerifyKeyIsAdded(keyName);

            m_SharedTableData.RemoveKey(keyName);
            Assert.IsFalse(m_SharedTableData.Contains(keyName), "Expected the key to not be contained when it has been removed from the shared table data.");
            Assert.IsNotNull(m_SharedTableData.AddKey(keyName), "Expected the key to added again after being removed, however it was not.");
        }

        [Test]
        public void KeyIsNotInSharedTableData_AfterRemovingKey_ByName()
        {
            const string keyName = "Remove Key By Name";
            AddAndVerifyKeyIsAdded(keyName);
            m_SharedTableData.RemoveKey(keyName);
            Assert.IsFalse(m_SharedTableData.Contains(keyName), "Expected the key to not be contained when it has been removed from the shared table data.");
        }

        [Test]
        public void KeyIsNotInSharedTableData_AfterRemovingKey_ById()
        {
            const string keyName = "Remove Key By Name";
            AddAndVerifyKeyIsAdded(keyName);
            uint addedKeyId = GetKeyIdAndVerifyItIsValid(keyName);

            m_SharedTableData.RemoveKey(addedKeyId);
            Assert.IsFalse(m_SharedTableData.Contains(keyName), "Expected the key to not be contained when it has been removed from the shared table data.");
        }

        [Test]
        public void RemovingInvalidKeyDoesNotChangeKeyCount_ByName()
        {
            var keyCount = m_SharedTableData.Entries.Count;
            m_SharedTableData.RemoveKey("Invalid Key");
            Assert.AreEqual(keyCount, m_SharedTableData.Entries.Count, "Expected the key count to be the same.");
        }

        [Test]
        public void RemovingInvalidKeyDoesNotChangeKeyCount_ById()
        {
            var keyCount = m_SharedTableData.Entries.Count;
            m_SharedTableData.RemoveKey(1234);
            Assert.AreEqual(keyCount, m_SharedTableData.Entries.Count, "Expected the key count to be the same.");
        }

        [TestCase("Start Name", "End Name")]
        [TestCase("12345", "11111")]
        [TestCase("Test Name {0}", "Test {0} Name {1}")]
        [TestCase("#MY_NAME#", "#MY_OTHER_NAME#")]
        [TestCase("#[][]<><>#", "#[0][0]<1><2>#")]
        [TestCase("", "not empty")]
        public void KeyHasTheSameIdAfterRename_UsingKeyToRename(string originalName, string newName)
        {
            AddAndVerifyKeyIsAdded(originalName);
            uint keyId = GetKeyIdAndVerifyItIsValid(originalName);

            m_SharedTableData.RenameKey(originalName, newName);
            Assert.AreEqual(keyId, GetKeyIdAndVerifyItIsValid(newName), "Expected renamed key to have the same id.");
        }

        [TestCase("Start Name", "End Name")]
        [TestCase("12345", "11111")]
        [TestCase("Test Name {0}", "Test {0} Name {1}")]
        [TestCase("#MY_NAME#", "#MY_OTHER_NAME#")]
        [TestCase("#[][]<><>#", "#[0][0]<1><2>#")]
        public void KeyHasTheSameIdAfterRename_UsingKeyIdToRename(string originalName, string newName)
        {
            AddAndVerifyKeyIsAdded(originalName);
            uint keyId = GetKeyIdAndVerifyItIsValid(originalName);

            m_SharedTableData.RenameKey(keyId, newName);
            Assert.AreEqual(keyId, GetKeyIdAndVerifyItIsValid(newName), "Expected renamed key to have the same id.");
        }
    }
}
