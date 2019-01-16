﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NPSBDummyLib
{
    public enum TestCase
    {
        ONLY_CONNECT = 1,
        REPEAT_CONNECT = 2,
        ECHO = 3,
        ECHO_CONNECT_DISCONNECT = 4,
        ECHO_CONNECT_DISCONNECT_RANDOM = 5,
        ECHO_CONNECT_DISCONNECT_SERVER = 6,
    }


    public partial class DummyManager
    {
        public async Task TestConnectOnlyAsync(Int64 index, TestCase testType)
        {            
            var testResults = new List<Task<(bool, string)>>();

            for (int i = 0; i < DummyList.Count; ++i)
            {
                var dummy = DummyList[i];
                testResults.Add(Task<(bool, string)>.Run(() => NetActionConnect.ConnectOnlyAsync(dummy, Config, IsInProgress)));
            }

            await Task.WhenAll(testResults.ToArray());


            for (int i = 0; i < DummyList.Count; ++i)
            {
                var dummy = DummyList[i];
                if(dummy.ConnectCount > 0)
                {
                    dummy.SetSuccess(true);
                }
            }

            TestResultMgr.AddTestResult(index, testType, DummyList);
        }

        public async Task TestRepeatConnectAsync()
        {
            var testResults = new List<Task<(bool, string)>>();

            for (int i = 0; i < DummyList.Count; ++i)
            {
                var dummy = DummyList[i];
                testResults.Add(Task<(bool, string)>.Run(() => NetActionConnect.RepeatConnectAsync(dummy, Config, IsInProgress)));
            }

            await Task.WhenAll(testResults.ToArray());
        }
    }
}
