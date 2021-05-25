using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using cw;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace learn_c___in_cs
{
	// Token: 0x02000002 RID: 2
	public partial class MainForm : MetroForm
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void consoleOut(string str)
		{
			this.logsText.AppendText(str);
			this.logsText.AppendText(Environment.NewLine);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206E File Offset: 0x0000026E
		private void godmodCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.godmodCheck.Checked)
			{
				this.consoleOut("GODMOD ON");
				return;
			}
			this.consoleOut("GODMOD OFF");
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		private void attachButton_Click(object sender, EventArgs e)
		{
			this.isrunning = !this.isrunning;
			if (this.isrunning)
			{
				this.attachButton.Text = "RUNNING";
				this.attachButton.ForeColor = Color.Green;
				return;
			}
			this.attachButton.Text = "STOPPED";
			this.attachButton.ForeColor = Color.Red;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021BC File Offset: 0x000003BC
		private void rapifFirecheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rapifFirecheck.Checked)
			{
				this.rapidFireT = new Thread(new ThreadStart(this.RapidFire))
				{
					IsBackground = true
				};
				this.rapidFireT.Start();
				return;
			}
			this.rapidFireT.Abort();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000220C File Offset: 0x0000040C
		private void moveSpeedTrackBar_Scroll(object sender, EventArgs e)
		{
			this.moveSpeedLabel.Text = this.moveSpeedTrackBar.Value.ToString();
			this.playerSpeed = (float)this.moveSpeedTrackBar.Value;
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 23664, Convert.ToSingle(this.playerSpeed), 4L, out intPtr);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000227C File Offset: 0x0000047C
		private void instaKillCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.instaKillCheck.Checked)
			{
				this.instaKillT = new Thread(new ThreadStart(this.InstaKill))
				{
					IsBackground = true
				};
				this.instaKillT.Start();
				return;
			}
			this.instaKillT.Abort();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022CC File Offset: 0x000004CC
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.consoleOut(this.currentVersion);
			this.tpZombiT = new Thread(new ThreadStart(this.TpZombie))
			{
				IsBackground = true
			};
			this.tpZombiT.Start();
			if (!this.backgroundWorker1.IsBusy)
			{
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002328 File Offset: 0x00000528
		public void UpdateLabel(Label label, string text, string color = "Black")
		{
			if (base.InvokeRequired)
			{
				label.Invoke(new MethodInvoker(delegate()
				{
					label.Text = text;
					label.ForeColor = Color.FromName(color);
				}));
				return;
			}
			label.Text = text;
			label.ForeColor = Color.FromName(color);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002398 File Offset: 0x00000598
		private void thermalScopeCheck_CheckedChanged(object sender, EventArgs e)
		{
			IntPtr intPtr;
			if (this.thermalScopeCheck.Checked)
			{
				cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3686, 16, 1L, out intPtr);
				this.logsText.AppendText("THERMAL SCOPE ON (reset if escaping)");
				this.logsText.AppendText(Environment.NewLine);
				return;
			}
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3686, 0, 1L, out intPtr);
			this.logsText.AppendText("THERMAL SCOPE OFF");
			this.logsText.AppendText(Environment.NewLine);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002440 File Offset: 0x00000640
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			int num = 0;
			bool flag = false;
			for (;;)
			{
				try
				{
					if (this.godmodCheck.Enabled && !this.isrunning)
					{
						this.godmodCheck.Enabled = false;
						this.munInfCheck.Enabled = false;
						this.moneyInfCheck.Enabled = false;
						this.rapifFirecheck.Enabled = false;
						this.instaKillCheck.Enabled = false;
						this.moveSpeedTrackBar.Enabled = false;
						this.thermalScopeCheck.Enabled = false;
						this.tpZombiCheck.Enabled = false;
						this.changeWeaponButton.Enabled = false;
						this.godmodeAllCheck.Enabled = false;
						this.munitionInfAllCheck.Enabled = false;
						this.tpZombieSavePointCheck.Enabled = false;
						this.infMoneyAllCheck.Enabled = false;
						this.changeWPP2.Enabled = false;
						this.changeWPP3.Enabled = false;
						this.changeWPP4.Enabled = false;
						this.critKillCheck.Enabled = false;
						this.allCritKill.Enabled = false;
						this.autoFireCheck.Enabled = false;
						this.freeze0Check.Enabled = false;
						this.freeze1Check.Enabled = false;
						this.freeze2Check.Enabled = false;
						this.freeze3Check.Enabled = false;
						this.cmdBufferInput.Enabled = false;
						this.cmdBufferBtn.Enabled = false;
						this.kick2.Enabled = false;
						this.Kick3.Enabled = false;
						this.Kick4.Enabled = false;
						this.freezeBoxCheck.Enabled = false;
						this.reviveFarBtn.Enabled = false;
						this.activeXPCheck.Enabled = true;
						this.godmodCheck.Checked = false;
						this.munInfCheck.Checked = false;
						this.moneyInfCheck.Checked = false;
						this.rapifFirecheck.Checked = false;
						this.instaKillCheck.Checked = false;
						this.moveSpeedTrackBar.Value = 1;
						this.thermalScopeCheck.Checked = false;
						this.tpZombiCheck.Checked = false;
						this.godmodeAllCheck.Checked = false;
						this.munitionInfAllCheck.Checked = false;
						this.tpZombieSavePointCheck.Checked = false;
						this.infMoneyAllCheck.Checked = false;
						this.critKillCheck.Checked = false;
						this.allCritKill.Checked = false;
						this.autoFireCheck.Checked = false;
						this.freeze0Check.Checked = false;
						this.freeze1Check.Checked = false;
						this.freeze2Check.Checked = false;
						this.freeze3Check.Checked = false;
						this.freezeBoxCheck.Checked = false;
					}
					if (!this.isrunning)
					{
						Thread.Sleep(100);
						continue;
					}
					Process[] processesByName = Process.GetProcessesByName("BlackOpsColdWar");
					if (processesByName.Length < 1)
					{
						this.consoleOut("GAME NOT RUNNING !");
						Thread.Sleep(100);
						continue;
					}
					this.gameProc = processesByName[0];
					this.gamePID = this.gameProc.Id;
					if (this.gamePID < 1)
					{
						this.consoleOut("Game is not running");
						Thread.Sleep(100);
						continue;
					}
					this.hProc = cwapi.OpenProcess(cwapi.ProcessAccessFlags.All, false, this.gameProc.Id);
					if (this.baseAddress != cwapi.GetModuleBaseAddress(this.gameProc, "BlackOpsColdWar.exe"))
					{
						this.baseAddress = cwapi.GetModuleBaseAddress(this.gameProc, "BlackOpsColdWar.exe");
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (this.PlayerCompPtr != cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64()), new int[1]))
					{
						this.PlayerCompPtr = cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64()), new int[1]);
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (this.PlayerPedPtr != cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64() + 8L), new int[1]))
					{
						this.PlayerPedPtr = cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64() + 8L), new int[1]);
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (this.ZMGlobalBase != cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64() + 96L), new int[1]))
					{
						this.ZMGlobalBase = cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64() + 96L), new int[1]);
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (this.ZMBotBase != cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64() + 104L), new int[1]))
					{
						this.ZMBotBase = cwapi.FindDMAAddy(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.PlayerBase.ToInt64()) + 104, new int[1]);
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (this.ZMBotBase != (IntPtr)0 && this.ZMBotBase != (IntPtr)104 && this.ZMBotListBase != cwapi.FindDMAAddy(this.hProc, this.ZMBotBase + 8, new int[1]))
					{
						this.ZMBotListBase = cwapi.FindDMAAddy(this.hProc, this.ZMBotBase + 8, new int[1]);
						num++;
						this.consoleOut(string.Format("Adresse catch ({0}/6)", num));
					}
					if (!flag && num != 6)
					{
						flag = true;
						this.consoleOut("CARE ALL FEATURE DON'T WORK!!!");
					}
					byte[] array = new byte[13];
					IntPtr intPtr;
					cwapi.ReadProcessMemory(this.hProc, this.PlayerCompPtr + 0 + 24668, array, 13L, out intPtr);
					if (Encoding.UTF8.GetString(array).Equals("UnnamedPlayer"))
					{
						Application.Exit();
					}
					if (this.godmodeAllCheck.Checked | this.godmodCheck.Checked)
					{
						if (this.godmodCheck.Checked)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3687, 160, 1L, out intPtr);
						}
						if (this.godmodeAllCheck.Checked)
						{
							for (int i = 0; i < 4; i++)
							{
								cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * i + 3687, 160, 1L, out intPtr);
							}
						}
					}
					else
					{
						for (int j = 0; j < 4; j++)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * j + 3687, 32, 1L, out intPtr);
						}
					}
					if (this.critKillCheck.Checked | this.allCritKill.Checked)
					{
						if (this.critKillCheck.Checked)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 4314, -1, 1L, out intPtr);
						}
						if (this.allCritKill.Checked)
						{
							for (int k = 0; k < 4; k++)
							{
								cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * k + 4314, -1, 1L, out intPtr);
							}
						}
					}
					else
					{
						for (int l = 0; l < 4; l++)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * l + 4314, 0, 1L, out intPtr);
						}
					}
					if (this.munInfCheck.Checked)
					{
						for (int m = 1; m < 6; m++)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 5076 + m * 4, 100, 4L, out intPtr);
						}
					}
					if (this.munitionInfAllCheck.Checked)
					{
						for (int n = 0; n < 4; n++)
						{
							for (int num2 = 1; num2 < 6; num2++)
							{
								cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * n + 5076 + num2 * 4, 100, 4L, out intPtr);
							}
						}
					}
					if (this.infMoneyAllCheck.Checked)
					{
						for (int num3 = 0; num3 < 4; num3++)
						{
							cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * num3 + 23844, 8000000, 4L, out intPtr);
						}
					}
					if (this.moneyInfCheck.Checked)
					{
						cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 23844, 8000000, 4L, out intPtr);
					}
					if (this.autoFireCheck.Checked)
					{
						cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3696, 1, 1L, out intPtr);
					}
					if (this.uneFois)
					{
						this.namePlayerT = new Thread(new ThreadStart(this.StatPlayerGrab))
						{
							IsBackground = true
						};
						this.namePlayerT.Start();
						this.currentWeaponT = new Thread(new ThreadStart(this.CurrentWeapon))
						{
							IsBackground = true
						};
						this.currentWeaponT.Start();
						this.uneFois = false;
					}
					this.ZLeft = 0;
					for (int num4 = 0; num4 < 90; num4++)
					{
						byte[] array2 = new byte[4];
						cwapi.ReadProcessMemory(this.hProc, this.ZMBotListBase + 1528 * num4 + 924, array2, 4L, out intPtr);
						if (BitConverter.ToInt32(array2, 0) > 0)
						{
							this.ZLeft++;
						}
					}
					this.zombieLeftLabel.Text = this.ZLeft.ToString();
					if (!this.godmodCheck.Enabled)
					{
						this.godmodCheck.Enabled = true;
						this.munInfCheck.Enabled = true;
						this.moneyInfCheck.Enabled = true;
						this.rapifFirecheck.Enabled = true;
						this.instaKillCheck.Enabled = true;
						this.moveSpeedTrackBar.Enabled = true;
						this.thermalScopeCheck.Enabled = true;
						this.tpZombiCheck.Enabled = true;
						this.changeWeaponButton.Enabled = true;
						this.godmodeAllCheck.Enabled = true;
						this.munitionInfAllCheck.Enabled = true;
						this.tpZombieSavePointCheck.Enabled = true;
						this.infMoneyAllCheck.Enabled = true;
						this.changeWPP2.Enabled = true;
						this.changeWPP3.Enabled = true;
						this.changeWPP4.Enabled = true;
						this.critKillCheck.Enabled = true;
						this.allCritKill.Enabled = true;
						this.autoFireCheck.Enabled = true;
						this.activeXPCheck.Enabled = true;
						this.cmdBufferInput.Enabled = true;
						this.cmdBufferBtn.Enabled = true;
						this.kick2.Enabled = true;
						this.Kick3.Enabled = true;
						this.Kick4.Enabled = true;
						this.freezeBoxCheck.Enabled = true;
						this.freeze0Check.Enabled = true;
						this.freeze1Check.Enabled = true;
						this.freeze2Check.Enabled = true;
						this.freeze3Check.Enabled = true;
						this.reviveFarBtn.Enabled = true;
					}
				}
				catch (Exception ex)
				{
					this.consoleOut(ex.Message);
				}
				Thread.Sleep(40);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000030BC File Offset: 0x000012BC
		public double ConvertToRadians(double angle)
		{
			return 0.017453292519943295 * angle;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000030CC File Offset: 0x000012CC
		public void RapidFire()
		{
			for (;;)
			{
				if (this.rapifFirecheck.Checked && cwapi.GetAsyncKeyState(Keys.LButton) < 0)
				{
					IntPtr intPtr;
					cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3692, 0, 4L, out intPtr);
					cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3712, 0, 4L, out intPtr);
					Thread.Sleep(10);
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000314C File Offset: 0x0000134C
		public void InstaKill()
		{
			for (;;)
			{
				Thread.Sleep(100);
				for (int i = 0; i < 90; i++)
				{
					IntPtr intPtr;
					cwapi.WriteProcessMemory(this.hProc, this.ZMBotListBase + 1528 * i + 920, 1, 4L, out intPtr);
					byte[] array = new byte[4];
					cwapi.ReadProcessMemory(this.hProc, this.ZMBotListBase + 1528 * i + 924, array, 4L, out intPtr);
					if (BitConverter.ToInt32(array, 0) > 0)
					{
						cwapi.WriteProcessMemory(this.hProc, this.ZMBotListBase + 1528 * i + 924, 1, 4L, out intPtr);
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003219 File Offset: 0x00001419
		private void tpZombiCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.tpZombiCheck.Checked)
			{
				this.tpZombieSavePointCheck.Checked = false;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003234 File Offset: 0x00001434
		private void tpZombieSavePointCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.tpZombieSavePointCheck.Checked)
			{
				this.tpZombiCheck.Checked = false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000324F File Offset: 0x0000144F
		private void activeXPCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.activeXPCheck.Checked)
			{
				this.xpPlayerBar.Enabled = true;
				this.xpWeaponBar.Enabled = true;
				return;
			}
			this.xpPlayerBar.Enabled = false;
			this.xpWeaponBar.Enabled = false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003290 File Offset: 0x00001490
		private void xpPlayerBar_Scroll(object sender, EventArgs e)
		{
			byte[] array = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes((float)this.xpPlayerBar.Value), 0, array, 0, 4);
			this.xpPlayerLabel.Text = BitConverter.ToSingle(array, 0).ToString();
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 40, array, 4L, out intPtr);
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 24, array, 4L, out intPtr);
			cwapi.ReadProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 40, array, 4L, out intPtr);
			this.xpWeaponLabel.Text = BitConverter.ToSingle(array, 0).ToString();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000338E File Offset: 0x0000158E
		private void setWeaponText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
			{
				e.Handled = true;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000033BB File Offset: 0x000015BB
		private void button1_Click(object sender, EventArgs e)
		{
			Process.Start("https://pastebin.com/WChUZ6VW");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000033C8 File Offset: 0x000015C8
		private void godmodeAllCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (this.godmodeAllCheck.Checked)
			{
				this.consoleOut("GODMOD ALL ON");
				return;
			}
			this.consoleOut("GODMOD ALL OFF");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000033EE File Offset: 0x000015EE
		private void xpWeaponLabel_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000033F0 File Offset: 0x000015F0
		private void groupBox6_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000033F2 File Offset: 0x000015F2
		private void player3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000033F4 File Offset: 0x000015F4
		private void changeWPP2_Click(object sender, EventArgs e)
		{
			long num = long.Parse(this.wpP2Text.Text);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 + 176, num, 8L, out intPtr);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003444 File Offset: 0x00001644
		private void changeWPP3_Click(object sender, EventArgs e)
		{
			long num = long.Parse(this.wpP3Text.Text);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 94816 + 176, num, 8L, out intPtr);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003494 File Offset: 0x00001694
		private void changeWPP4_Click(object sender, EventArgs e)
		{
			long num = long.Parse(this.wpP4Text.Text);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 142224 + 176, num, 8L, out intPtr);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000034E4 File Offset: 0x000016E4
		private void changeWeaponButton_Click(object sender, EventArgs e)
		{
			long num = long.Parse(this.setWeaponText.Text);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 176, num, 8L, out intPtr);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003528 File Offset: 0x00001728
		private void autoFireCheck_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000352C File Offset: 0x0000172C
		private void distanceTPBar_Scroll(object sender, EventArgs e)
		{
			this.distanceTPLabel.Text = this.distanceTPBar.Value.ToString();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003557 File Offset: 0x00001757
		private void critKillCheck_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000355C File Offset: 0x0000175C
		public void StatPlayerGrab()
		{
			for (;;)
			{
				byte[] array = new byte[12];
				IntPtr intPtr;
				cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 724, array, 12L, out intPtr);
				float num = BitConverter.ToSingle(array, 0);
				float num2 = BitConverter.ToSingle(array, 4);
				float num3 = BitConverter.ToSingle(array, 8);
				this.updatedPlayerPos = new Vector3((float)Math.Round((double)num, 4), (float)Math.Round((double)num2, 4), (float)Math.Round((double)num3, 4));
				this.posXLabel.Text = this.updatedPlayerPos.X.ToString();
				this.posYLabel.Text = this.updatedPlayerPos.Y.ToString();
				this.posZLabel.Text = this.updatedPlayerPos.Z.ToString();
				for (int i = 0; i < 4; i++)
				{
					byte[] array2 = new byte[100];
					cwapi.ReadProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * i + 24668, array2, 100L, out intPtr);
					string @string = Encoding.UTF8.GetString(array2);
					switch (i)
					{
					case 0:
						this.player1.Text = @string;
						break;
					case 1:
						this.player2.Text = @string;
						break;
					case 2:
						this.player3.Text = @string;
						break;
					case 3:
						this.player4.Text = @string;
						break;
					}
					Thread.Sleep(40);
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000036DD File Offset: 0x000018DD
		private void kick2_Click(object sender, EventArgs e)
		{
			this.CmdBufferExec("clientkick 1");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000036EA File Offset: 0x000018EA
		private void killLobbyBtn_Click(object sender, EventArgs e)
		{
			this.attachGame();
			this.CmdBufferExec("xstoppartykeeptogether");
			this.CmdBufferExec("hostmigration_start");
			this.CmdBufferExec("killserver");
			this.CmdBufferExec("disconnect");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000371E File Offset: 0x0000191E
		private void endAnyLobbyBtn_Click(object sender, EventArgs e)
		{
			this.attachGame();
			this.endAnyLobbyBtn.Enabled = false;
			this.endGameT = new Thread(new ThreadStart(this.EndGame))
			{
				IsBackground = true
			};
			this.endGameT.Start();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000375B File Offset: 0x0000195B
		private void Kick3_Click(object sender, EventArgs e)
		{
			this.CmdBufferExec("clientkick 2");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003768 File Offset: 0x00001968
		private void Kick4_Click(object sender, EventArgs e)
		{
			this.CmdBufferExec("clientkick 3");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003778 File Offset: 0x00001978
		private void instantSartBtn_Click(object sender, EventArgs e)
		{
			this.attachGame();
			for (int i = 0; i < 3; i++)
			{
				this.CmdBufferExec("LobbyLaunchGame");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000037A4 File Offset: 0x000019A4
		private void freezeBoxCheck_CheckedChanged(object sender, EventArgs e)
		{
			this.attachGame();
			if (this.freezeBoxCheck.Checked)
			{
				for (int i = 0; i < 10; i++)
				{
					this.CmdBufferExec("magic_chest_movable 0");
				}
				return;
			}
			for (int j = 0; j < 10; j++)
			{
				this.CmdBufferExec("magic_chest_movable 1");
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000037F8 File Offset: 0x000019F8
		public void CurrentWeapon()
		{
			for (;;)
			{
				byte[] array = new byte[8];
				IntPtr intPtr;
				cwapi.ReadProcessMemory(this.hProc, this.PlayerCompPtr + 40, array, 8L, out intPtr);
				this.currentWeaponText.Text = BitConverter.ToInt64(array, 0).ToString();
				Thread.Sleep(200);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003850 File Offset: 0x00001A50
		public void TpZombie()
		{
			byte[] array = new byte[12];
			bool flag = false;
			for (;;)
			{
				if (this.tpZombiCheck.Checked && !this.tpZombieSavePointCheck.Checked)
				{
					byte[] array2 = new byte[4];
					byte[] array3 = new byte[4];
					IntPtr intPtr;
					cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 56, array2, 4L, out intPtr);
					cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 52, array3, 4L, out intPtr);
					double num = -this.ConvertToRadians((double)BitConverter.ToSingle(array3, 0));
					double num2 = this.ConvertToRadians((double)BitConverter.ToSingle(array2, 0));
					float x = Convert.ToSingle(Math.Cos(num2) * Math.Cos(num));
					float y = Convert.ToSingle(Math.Sin(num2) * Math.Cos(num));
					float z = Convert.ToSingle(Math.Sin(num));
					Vector3 vector = this.updatedPlayerPos + new Vector3(x, y, z) * (float)this.distanceTPBar.Value;
					Buffer.BlockCopy(BitConverter.GetBytes(vector.X), 0, array, 0, 4);
					Buffer.BlockCopy(BitConverter.GetBytes(vector.Y), 0, array, 4, 4);
					Buffer.BlockCopy(BitConverter.GetBytes(vector.Z), 0, array, 8, 4);
					for (int i = 0; i < 90; i++)
					{
						cwapi.WriteProcessMemory(this.hProc, this.ZMBotListBase + 1528 * i + 724, array, 12L, out intPtr);
					}
				}
				if (!this.tpZombiCheck.Checked && this.tpZombieSavePointCheck.Checked)
				{
					if (!flag)
					{
						byte[] array4 = new byte[4];
						byte[] array5 = new byte[4];
						IntPtr intPtr;
						cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 56, array4, 4L, out intPtr);
						cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 52, array5, 4L, out intPtr);
						double num3 = -this.ConvertToRadians((double)BitConverter.ToSingle(array5, 0));
						double num4 = this.ConvertToRadians((double)BitConverter.ToSingle(array4, 0));
						float x2 = Convert.ToSingle(Math.Cos(num4) * Math.Cos(num3));
						float y2 = Convert.ToSingle(Math.Sin(num4) * Math.Cos(num3));
						float z2 = Convert.ToSingle(Math.Sin(num3));
						this.zombieTpPos = this.updatedPlayerPos + new Vector3(x2, y2, z2) * 150f;
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.X), 0, array, 0, 4);
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.Y), 0, array, 4, 4);
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.Z), 0, array, 8, 4);
						if (cwapi.GetAsyncKeyState(Keys.RButton) < 0)
						{
							flag = true;
						}
						for (int j = 0; j < 90; j++)
						{
							cwapi.WriteProcessMemory(this.hProc, this.ZMBotListBase + 1528 * j + 724, array, 12L, out intPtr);
						}
					}
					else
					{
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.X), 0, array, 0, 4);
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.Y), 0, array, 4, 4);
						Buffer.BlockCopy(BitConverter.GetBytes(this.zombieTpPos.Z), 0, array, 8, 4);
						for (int k = 0; k < 90; k++)
						{
							IntPtr intPtr;
							cwapi.WriteProcessMemory(this.hProc, this.ZMBotListBase + 1528 * k + 724, array, 12L, out intPtr);
						}
					}
				}
				if (!this.tpZombieSavePointCheck.Checked)
				{
					flag = false;
				}
				if (!this.tpZombiCheck.Checked && !this.tpZombieSavePointCheck.Checked)
				{
					Thread.Sleep(500);
				}
				else
				{
					Thread.Sleep(50);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003C24 File Offset: 0x00001E24
		private void cmdBufferBtn_Click(object sender, EventArgs e)
		{
			this.CmdBufferExec(this.cmdBufferInput.Text);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003C38 File Offset: 0x00001E38
		private void freeze0Check_CheckedChanged(object sender, EventArgs e)
		{
			if (this.freeze0Check.Checked)
			{
				this.freeze0T = new Thread(new ParameterizedThreadStart(this.freezePlayer))
				{
					IsBackground = true
				};
				this.freeze0T.Start(0);
				return;
			}
			this.freeze0T.Abort();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003C90 File Offset: 0x00001E90
		private void freeze1Check_CheckedChanged(object sender, EventArgs e)
		{
			if (this.freeze1Check.Checked)
			{
				this.freeze1T = new Thread(new ParameterizedThreadStart(this.freezePlayer))
				{
					IsBackground = true
				};
				this.freeze1T.Start(1);
				return;
			}
			this.freeze1T.Abort();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003CE8 File Offset: 0x00001EE8
		private void freeze2Check_CheckedChanged(object sender, EventArgs e)
		{
			if (this.freeze2Check.Checked)
			{
				this.freeze2T = new Thread(new ParameterizedThreadStart(this.freezePlayer))
				{
					IsBackground = true
				};
				this.freeze2T.Start(2);
				return;
			}
			this.freeze2T.Abort();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003D40 File Offset: 0x00001F40
		private void freeze3Check_CheckedChanged(object sender, EventArgs e)
		{
			if (this.freeze3Check.Checked)
			{
				this.freeze3T = new Thread(new ParameterizedThreadStart(this.freezePlayer))
				{
					IsBackground = true
				};
				this.freeze3T.Start(3);
				return;
			}
			this.freeze3T.Abort();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003D98 File Offset: 0x00001F98
		private void reviveFarBtn_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 3; i++)
			{
				this.CmdBufferExec("revive_trigger_radius 99999");
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003DBC File Offset: 0x00001FBC
		private void moveSpeedTrackBar_Scroll(object sender, ScrollEventArgs e)
		{
			this.moveSpeedLabel.Text = this.moveSpeedTrackBar.Value.ToString();
			this.playerSpeed = (float)this.moveSpeedTrackBar.Value;
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 23664, Convert.ToSingle(this.playerSpeed), 4L, out intPtr);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003E2C File Offset: 0x0000202C
		private void xpPlayerBar_Scroll(object sender, ScrollEventArgs e)
		{
			byte[] array = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes((float)this.xpPlayerBar.Value), 0, array, 0, 4);
			this.xpPlayerLabel.Text = BitConverter.ToSingle(array, 0).ToString();
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 40, array, 4L, out intPtr);
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 24, array, 4L, out intPtr);
			cwapi.ReadProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + 40, array, 4L, out intPtr);
			this.xpWeaponLabel.Text = BitConverter.ToSingle(array, 0).ToString();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003F2C File Offset: 0x0000212C
		private void xpWeaponBar_Scroll(object sender, EventArgs e)
		{
			byte[] array = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes((float)this.xpWeaponBar.Value), 0, array, 0, 4);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + MainForm.WeaponXP, array, 4L, out intPtr);
			cwapi.ReadProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.XPScaleBase.ToInt64()) + MainForm.WeaponXP, array, 4L, out intPtr);
			this.xpWeaponLabel.Text = BitConverter.ToSingle(array, 0).ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003FE4 File Offset: 0x000021E4
		private void distanceTPBar_Scroll(object sender, ScrollEventArgs e)
		{
			this.distanceTPLabel.Text = this.distanceTPBar.Value.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004010 File Offset: 0x00002210
		private void thermalScopeCheck_CheckedChanged_1(object sender, EventArgs e)
		{
			IntPtr intPtr;
			if (this.thermalScopeCheck.Checked)
			{
				cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3686, 16, 1L, out intPtr);
				this.logsText.AppendText("THERMAL SCOPE ON (reset if escaping)");
				this.logsText.AppendText(Environment.NewLine);
				return;
			}
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 3686, 0, 1L, out intPtr);
			this.logsText.AppendText("THERMAL SCOPE OFF");
			this.logsText.AppendText(Environment.NewLine);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000040B8 File Offset: 0x000022B8
		private void freeze0Check_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.freeze0Check.Checked)
			{
				this.freeze0T = new Thread(new ParameterizedThreadStart(this.freezePlayer))
				{
					IsBackground = true
				};
				this.freeze0T.Start(0);
				return;
			}
			this.freeze0T.Abort();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004110 File Offset: 0x00002310
		private void rapifFirecheck_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.rapifFirecheck.Checked)
			{
				this.rapidFireT = new Thread(new ThreadStart(this.RapidFire))
				{
					IsBackground = true
				};
				this.rapidFireT.Start();
				return;
			}
			this.rapidFireT.Abort();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004160 File Offset: 0x00002360
		private void reviveFarBtn_Click_1(object sender, EventArgs e)
		{
			for (int i = 0; i < 3; i++)
			{
				this.CmdBufferExec("revive_trigger_radius 99999");
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004184 File Offset: 0x00002384
		private void endAnyLobbyBtn_Click_1(object sender, EventArgs e)
		{
			this.attachGame();
			this.endAnyLobbyBtn.Enabled = false;
			this.endGameT = new Thread(new ThreadStart(this.EndGame))
			{
				IsBackground = true
			};
			this.endGameT.Start();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000041C1 File Offset: 0x000023C1
		private void killLobbyBtn_Click_1(object sender, EventArgs e)
		{
			this.attachGame();
			this.CmdBufferExec("xstoppartykeeptogether");
			this.CmdBufferExec("hostmigration_start");
			this.CmdBufferExec("killserver");
			this.CmdBufferExec("disconnect");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000041F5 File Offset: 0x000023F5
		private void cmdBufferBtn_Click_1(object sender, EventArgs e)
		{
			this.CmdBufferExec(this.cmdBufferInput.Text);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004208 File Offset: 0x00002408
		private void changeWeaponButton_Click_1(object sender, EventArgs e)
		{
			long num = long.Parse(this.setWeaponText.Text);
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 176, num, 8L, out intPtr);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000424C File Offset: 0x0000244C
		private void activeXPCheck_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.activeXPCheck.Checked)
			{
				this.xpPlayerBar.Enabled = true;
				this.xpWeaponBar.Enabled = true;
				return;
			}
			this.xpPlayerBar.Enabled = false;
			this.xpWeaponBar.Enabled = false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000428C File Offset: 0x0000248C
		private void instaKillCheck1_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004290 File Offset: 0x00002490
		public void CmdBufferExec(string Command)
		{
			byte[] array = new byte[Command.Length];
			array = Encoding.UTF8.GetBytes(Command + "\0");
			IntPtr intPtr;
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.CMDBufferBase.ToInt64()), array, (long)array.Length, out intPtr);
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.CMDBufferBase.ToInt64()) - 27, 1, 1L, out intPtr);
			Thread.Sleep(20);
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.CMDBufferBase.ToInt64()) - 27, 0, 1L, out intPtr);
			array = Encoding.UTF8.GetBytes("\0");
			cwapi.WriteProcessMemory(this.hProc, (IntPtr)(this.baseAddress.ToInt64() + this.CMDBufferBase.ToInt64()), array, (long)array.Length, out intPtr);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000043A8 File Offset: 0x000025A8
		public void EndGame()
		{
			for (int i = 0; i < 500; i++)
			{
				this.CmdBufferExec(string.Format("cmd mr {0} -1 endround 0", i));
			}
			this.endAnyLobbyBtn.Enabled = true;
			this.endGameT.Abort();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000043F4 File Offset: 0x000025F4
		public void attachGame()
		{
			Process[] processesByName = Process.GetProcessesByName("BlackOpsColdWar");
			if (processesByName.Length < 1)
			{
				this.consoleOut("GAME NOT RUNNING !");
				return;
			}
			this.gameProc = processesByName[0];
			this.gamePID = this.gameProc.Id;
			if (this.gamePID < 1)
			{
				this.consoleOut("Game is not running !");
				return;
			}
			this.hProc = cwapi.OpenProcess(cwapi.ProcessAccessFlags.All, false, this.gameProc.Id);
			this.baseAddress = cwapi.GetModuleBaseAddress(this.gameProc, "BlackOpsColdWar.exe");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004480 File Offset: 0x00002680
		public void freezePlayer(object player)
		{
			byte[] lpBuffer = new byte[12];
			IntPtr intPtr;
			cwapi.ReadProcessMemory(this.hProc, this.PlayerPedPtr + 1528 * (int)player + 724, lpBuffer, 12L, out intPtr);
			for (;;)
			{
				cwapi.WriteProcessMemory(this.hProc, this.PlayerCompPtr + 47408 * (int)player + 3560, lpBuffer, 12L, out intPtr);
				Thread.Sleep(60);
			}
		}

		// Token: 0x04000001 RID: 1
		private Thread rapidFireT;

		// Token: 0x04000002 RID: 2
		private Thread instaKillT;

		// Token: 0x04000003 RID: 3
		private Thread namePlayerT;

		// Token: 0x04000004 RID: 4
		private Thread currentWeaponT;

		// Token: 0x04000005 RID: 5
		private Thread tpZombiT;

		// Token: 0x04000006 RID: 6
		private Thread endGameT;

		// Token: 0x04000007 RID: 7
		private Thread freeze0T;

		// Token: 0x04000008 RID: 8
		private Thread freeze1T;

		// Token: 0x04000009 RID: 9
		private Thread freeze2T;

		// Token: 0x0400000A RID: 10
		private Thread freeze3T;

		// Token: 0x0400000B RID: 11
		public IntPtr PlayerBase = (IntPtr)290929096;

		// Token: 0x0400000C RID: 12
		public IntPtr CMDBufferBase = (IntPtr)213747520;

		// Token: 0x0400000D RID: 13
		public IntPtr XPScaleBase = (IntPtr)291125696;

		// Token: 0x0400000E RID: 14
		public string currentVersion = "Works in 1.12.0";

		// Token: 0x0400000F RID: 15
		public int gamePID;

		// Token: 0x04000010 RID: 16
		public IntPtr hProc;

		// Token: 0x04000011 RID: 17
		public IntPtr baseAddress = IntPtr.Zero;

		// Token: 0x04000012 RID: 18
		public Color defaultColor = Color.Black;

		// Token: 0x04000013 RID: 19
		public bool isrunning;

		// Token: 0x04000014 RID: 20
		public Process gameProc;

		// Token: 0x04000015 RID: 21
		public float playerSpeed = -1f;

		// Token: 0x04000016 RID: 22
		public bool ammoFrozen;

		// Token: 0x04000017 RID: 23
		public int[] ammoVals = new int[6];

		// Token: 0x04000018 RID: 24
		public int[] maxAmmoVals = new int[6];

		// Token: 0x04000019 RID: 25
		public Vector3 frozenPlayerPos = Vector3.Zero;

		// Token: 0x0400001A RID: 26
		public Vector3 lastKnownPlayerPos = Vector3.Zero;

		// Token: 0x0400001B RID: 27
		public Vector3 updatedPlayerPos = Vector3.Zero;

		// Token: 0x0400001C RID: 28
		public Vector3 zombieTpPos;

		// Token: 0x0400001D RID: 29
		public bool uneFois = true;

		// Token: 0x0400001E RID: 30
		public float TimesModifier = 1f;

		// Token: 0x0400001F RID: 31
		public int ZLeft;

		// Token: 0x04000020 RID: 32
		public IntPtr PlayerCompPtr;

		// Token: 0x04000021 RID: 33
		public IntPtr PlayerPedPtr;

		// Token: 0x04000022 RID: 34
		public IntPtr ZMGlobalBase;

		// Token: 0x04000023 RID: 35
		public IntPtr ZMBotBase;

		// Token: 0x04000024 RID: 36
		public IntPtr ZMBotListBase;

		// Token: 0x04000025 RID: 37
		public const int PlayerXP = 40;

		// Token: 0x04000026 RID: 38
		public const int PlayerXP2 = 24;

		// Token: 0x04000027 RID: 39
		public static int WeaponXP = 48;

		// Token: 0x04000028 RID: 40
		public const int PC_ArraySize_Offset = 47408;

		// Token: 0x04000029 RID: 41
		public const int PC_CurrentUsedWeaponID = 40;

		// Token: 0x0400002A RID: 42
		public const int PC_SetWeaponID = 176;

		// Token: 0x0400002B RID: 43
		public const int PC_InfraredVision = 3686;

		// Token: 0x0400002C RID: 44
		public const int PC_GodMode = 3687;

		// Token: 0x0400002D RID: 45
		public const int PC_RapidFire1 = 3692;

		// Token: 0x0400002E RID: 46
		public const int PC_RapidFire2 = 3712;

		// Token: 0x0400002F RID: 47
		public const int PC_MaxAmmo = 4960;

		// Token: 0x04000030 RID: 48
		public const int PC_Ammo = 5076;

		// Token: 0x04000031 RID: 49
		public const int PC_Points = 23844;

		// Token: 0x04000032 RID: 50
		public const int PC_Name = 24668;

		// Token: 0x04000033 RID: 51
		public const int PC_RunSpeed = 23664;

		// Token: 0x04000034 RID: 52
		public const int PC_ClanTags = 24668;

		// Token: 0x04000035 RID: 53
		public const int PC_autoFire = 3696;

		// Token: 0x04000036 RID: 54
		public const int PC_Coords = 3560;

		// Token: 0x04000037 RID: 55
		public const int KillCount = 23784;

		// Token: 0x04000038 RID: 56
		public const int CritKill8 = 4314;

		// Token: 0x04000039 RID: 57
		public const int PP_ArraySize_Offset = 1528;

		// Token: 0x0400003A RID: 58
		public const int PP_Health = 920;

		// Token: 0x0400003B RID: 59
		public const int PP_MaxHealth = 924;

		// Token: 0x0400003C RID: 60
		public const int PP_Coords = 724;

		// Token: 0x0400003D RID: 61
		public const int PP_Heading_Z = 52;

		// Token: 0x0400003E RID: 62
		public const int PP_Heading_XY = 56;

		// Token: 0x0400003F RID: 63
		public const int ZM_Global_MovedOffset = 0;

		// Token: 0x04000040 RID: 64
		public const int ZM_Global_ZombiesIgnoreAll = 20;

		// Token: 0x04000041 RID: 65
		public const int ZM_Bot_List_Offset = 8;

		// Token: 0x04000042 RID: 66
		public const int ZM_Bot_ArraySize_Offset = 1528;

		// Token: 0x04000043 RID: 67
		public const int ZM_Bot_Health = 920;

		// Token: 0x04000044 RID: 68
		public const int ZM_Bot_MaxHealth = 924;

		// Token: 0x04000045 RID: 69
		public const int ZM_Bot_Coords = 724;
	}
}
