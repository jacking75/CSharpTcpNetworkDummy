﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TcpDummyClient
{
    public partial class MainForm : Form
    {
        NPSBDummyLib.DummyManager DummyManager = new NPSBDummyLib.DummyManager();
        
        System.Collections.Concurrent.ConcurrentQueue<string> logMsgQueue;

        System.Windows.Threading.DispatcherTimer dispatcherUITimer;
        System.Windows.Threading.DispatcherTimer dispatcherLogTimer;


        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        void Init()
        {
            DummyManager.LogFunc = AddLog;
            
            logMsgQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
            

            dispatcherUITimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherUITimer.Tick += new EventHandler(UpdateUI);
            dispatcherUITimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherUITimer.Start();

            dispatcherLogTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherLogTimer.Tick += new EventHandler(UpdateLogPrint);
            dispatcherLogTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherLogTimer.Start();
        }

        void AddLog(string msg)
        {
            logMsgQueue.Enqueue(msg);
        }

        #region 테스트 설정 값
        (int, string, int) GetTestConfig()
        {
            return (textBoxConnectCount.Text.ToInt32(), textBoxIP.Text, textBoxPort.Text.ToInt32());
        }
        /*
        TcpDummyClientsLib.TestRepeatConnDisConnConfig GetTestRepeatConnDisConnConfig()
        {
            return new TcpDummyClientsLib.TestRepeatConnDisConnConfig
            {
                DummyCount = textBox4.Text.ToInt32(),
                RemoteIP = textBoxIP.Text,
                RemotePort = textBoxPort.Text.ToUInt16(),

                RepeatCount = textBox5.Text.ToInt32(),
                RepeatTime = DateTime.Now.AddSeconds(textBox2.Text.ToInt32()),
            };
        }
                
        TcpDummyClientsLib.TestSimpleEchoConfig GetTestTestSimpleEchoConfig()
        {
            return new TcpDummyClientsLib.TestSimpleEchoConfig
            {
                DummyCount = textBox9.Text.ToInt32(),
                RemoteIP = textBoxIP.Text,
                RemotePort = textBoxPort.Text.ToUInt16(),

                RepeatCount = textBox8.Text.ToInt32(),
                RepeatTime = DateTime.Now.AddSeconds(textBox7.Text.ToInt32()),

                MinSendDataSize = textBox10.Text.ToInt32(),
                MaxSendDataSize = textBox11.Text.ToInt32(),

                LogFunc = AddLog,
            };
        }
        */
        #endregion


        #region 접속만하기
        // 접속만.... - 접속 하기
        private async void button1_Click(object sender, EventArgs e)
        {
            (var conCount, var ip, var port) = GetTestConfig();
            var config = new NPSBDummyLib.TestConfig
            {
                RmoteIP = ip,
                RemotePort = port,
                DummyCount = conCount,
            };
            DummyManager.Prepare(config);

            await DummyManager.TestConnectOnlyAsync(DateTime.Now.Ticks, NPSBDummyLib.TestCase.ONLY_CONNECT);
            //NPSBDummyLib.D
            //var result = await Task.Run(async () => await DummyConnectOnly.ProcessAsync(config.Item2, config.Item3));

            //AddLog(result);
        }

        // 접속만.... - 접속 끊기
        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            //var result = await Task.Run(async () => await DummyConnectOnly.EndAsync());
            //AddLog(result);
        }
        #endregion

        #region 접속/끊기 반복
        // 테스트 시작 - 접속/끊기 반복
        private async void button4_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            //var config = GetTestRepeatConnDisConnConfig();

            if (checkBox1.Checked == false)
            {
                //var result = await Task.Run(async () => await DummyRepeatConnDisConn.Start(config.DummyCount, config.RepeatCount, config.RepeatTime, config.RemoteIP, config.RemotePort));
                //AddLog(result);
            }
            else
            {
                //var result = await Task.Run(async () => await DummyRepeatConnDisConnSend.Start(config.DummyCount, config.RepeatCount, config.RepeatTime, config.RemoteIP, config.RemotePort));
                //AddLog(result);
            }
        }

        // 테스트 중단 - 접속/끊기 반복
        private async void button3_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            if (checkBox1.Checked == false)
            {
                //var result = await Task.Run(async () => await DummyRepeatConnDisConn.End());
                //AddLog(result);
            }
            else
            {
                //var result = await Task.Run(async () => await DummyRepeatConnDisConnSend.End());
                //AddLog(result);
            }
        }
        #endregion


        #region 에코 테스트
        private async void button9_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            //var config = GetTestTestSimpleEchoConfig();
            //var result = await Task.Run(async () => await DummySimpleEcho.Start(config));

            //AddLog(result);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            //var result = await Task.Run(async () => await DummySimpleEcho.End());

            //AddLog(result);
        }
        #endregion


        #region Test
        // Test 연결
        private void button5_Click(object sender, EventArgs e)
        {
            //var config = GetTestConfig();

            //var result = DevTestgSendReceive.Connect(config.RemoteIP, config.RemotePort);

            //AddLog(result);
        }

        // Test 접속 끊기
        private void button6_Click(object sender, EventArgs e)
        {
            //DevTestgSendReceive.Close();

            //AddLog("서버와 접속 끊음");
        }

        // Test 보내기
        private void button7_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(textBox6.Text))
            //{
            //    return;
            //}

            //DevTestgSendReceive.SendData(textBox6.Text);
        }
        #endregion


        void UpdateUI(object sender, EventArgs e)
        {
            try
            {
                //textBox1.Text = DummyConnectOnly.CurrentConnectedCount().ToString();
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
        }

        void UpdateLogPrint(object sender, EventArgs e)
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (logMsgQueue.TryDequeue(out msg) == false)
                {
                    break;
                }

                ++logWorkCount;

                if (listBoxLog.Items.Count > 512)
                {
                    listBoxLog.Items.Clear();
                }

                listBoxLog.Items.Add(msg);
                listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;

                if (logWorkCount > 16)
                {
                    break;
                }
            }
        }


    }
}
