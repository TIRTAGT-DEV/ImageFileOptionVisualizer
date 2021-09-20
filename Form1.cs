using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace ImageFileOptionVisualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void IFEOEnableOnlyMode_CheckedChanged(object sender, EventArgs e)
        {
            if (IFEOEnabledOnlyMode.Checked)
            {
                AvastCleanupOnlyMode.Checked = false;
                AnythingMode.Checked = false;
            }
        }

        private void AvastCleanupOnlyMode_CheckedChanged(object sender, EventArgs e)
        {
            if (AvastCleanupOnlyMode.Checked)
            {
                IFEOEnabledOnlyMode.Checked = false;
                AnythingMode.Checked = false;
            }
        }

        private void AnythingMode_CheckedChanged(object sender, EventArgs e)
        {
            if (AnythingMode.Checked)
            {
                IFEOEnabledOnlyMode.Checked = false;
                AvastCleanupOnlyMode.Checked = false;
            }
        }

        private void RefreshTreeDisplay_Click(object sender, EventArgs e)
        {
            // Clear the TreeDisplay first
            DisplayTreeView.Nodes.Clear();

            // Load registry
            RegistryKey IFEORoot;
            try
            {
                IFEORoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", false);
            }
            catch (System.Security.SecurityException _)
            {
                MessageBox.Show("Unable to open the LocalMachine registry\n" + _.ToString(), "Access Denied");
                return;
            }
            
            if (IFEORoot == null)
            {
                MessageBox.Show("There are no Image File Execution Option Folder in the registry.");
                return;
            }

            string[] IFEOApps = IFEORoot.GetSubKeyNames();

            foreach (string key in IFEOApps)
            {
                bool IsFiltered = false;

                // If avast only filter are enabled, set up default filtering.
                if (AvastCleanupOnlyMode.Checked || IFEOEnabledOnlyMode.Checked)
                {
                    IsFiltered = true;
                }

                TreeNode AppNode = new TreeNode
                {
                    Text = key,
                    Tag = "AppNode"
                };

                RegistryKey IFEOPolicyRoot = null;

                try
                {
                    IFEOPolicyRoot = IFEORoot.OpenSubKey(key, false);
                }
                catch (System.Security.SecurityException)
                {
                    DisplayTreeView.Nodes.Add(new TreeNode { 
                        Text = "[ ACCESS DENIED ] - " + key
                    });
                    continue;
                }

                if (IFEOPolicyRoot == null)
                {
                    MessageBox.Show("There are no IFEO Root in the registry.");
                    return;
                }

                string[] IFEOPolicies = IFEOPolicyRoot.GetSubKeyNames();

                foreach (string Policy in IFEOPolicies)
                {
                    TreeNode AppPolicyNode = new TreeNode
                    {
                        Text = Policy,
                        Tag = "AppPolicyNode"
                    };

                    RegistryKey PolicyValueRoot = null;

                    try
                    {
                        PolicyValueRoot = IFEOPolicyRoot.OpenSubKey(Policy, false);
                    }
                    catch (System.Security.SecurityException _)
                    {
                        MessageBox.Show("Unable to open the IFEO Policy\n" + _.ToString(), "Access Denied");
                        continue;
                    }

                    // Display a message if there is no IFEO Policy in the registry
                    if (PolicyValueRoot == null)
                    {
                        MessageBox.Show("There are no IFEO Policy in the registry.");
                        return;
                    }

                    string[] PolicyNames = PolicyValueRoot.GetValueNames();

                    foreach (string Names in PolicyNames)
                    {
                        if (IFEOEnabledOnlyMode.Checked)
                        {
                            IsFiltered = false;
                        }

                        TreeNode AppValueNode = new TreeNode
                        {
                            Text = Names,
                            Tag = "AppValueNode"
                        };

                        string NameValue = PolicyValueRoot.GetValue(Names).ToString();

                        if (NameValue == null)
                        {
                            continue;
                        }
                        else if (AvastCleanupOnlyMode.Checked && Names == "Debugger" && NameValue.Contains("Avast Software\\Cleanup"))
                        {
                            IsFiltered = false;
                        }

                        AppValueNode.Text += " : " + NameValue;

                        AppPolicyNode.Nodes.Add(AppValueNode);
                    }

                    if (PolicyValueRoot != null)
                    {
                        PolicyValueRoot.Close();
                    }

                    AppNode.Nodes.Add(AppPolicyNode);
                }

                if (IFEOPolicyRoot != null)
                {
                    IFEOPolicyRoot.Close();
                }

                // If not filtered, then add it to the display
                if (!IsFiltered)
                {
                    DisplayTreeView.Nodes.Add(AppNode);
                }
            }

            if (IFEORoot != null)
            {
                IFEORoot.Close();
            }

            // Display count detected
            EntryCountDisplay.Text = "Entry Count : " + DisplayTreeView.Nodes.Count.ToString();
        }

        private void RemoveSelectedEntry_Click(object sender, EventArgs e)
        {
            RemoveSelectedEntry_Click(false);
            RefreshTreeDisplay_Click(sender, e);
        }

        private bool RemoveSelectedEntry_Click(bool BypassSecurityQuestions)
        {
            if (DisplayTreeView.SelectedNode == null) { return false; }
            TreeNode SelectedNode = DisplayTreeView.SelectedNode;

            DialogResult UserResponse;
            
            if (BypassSecurityQuestions) {
                UserResponse = DialogResult.Yes;   
            }
            else
            {
                UserResponse = MessageBox.Show("You sure that you want to remove the IFEO of\n    " + SelectedNode.Text + "\n\nMake sure you know what you are doing, modifying registry is highly dangereous.\n\nDid you want to proceed removing ?", "Remove Selected IFEO Confirmation Dialog", MessageBoxButtons.YesNoCancel);
            }

            if (UserResponse == DialogResult.Yes)
            {
                // User want to proceed, try to remove the selected IFEO.
                string NodeTag = SelectedNode.Tag.ToString();

                // Do not continue if Policy Value was selected.
                if (NodeTag == "AppValueNode")
                {
                    MessageBox.Show("Sorry, you can't remove the IFEO value, try removing the policy root of this value instead.", "[ ERROR ] User is trying to remove Policy Value instead of Policy Root");
                    return false;
                }


                RegistryKey IFEORoot;
                string PolicyPath;

                if (NodeTag == "AppPolicyNode")
                {
                    // Policy selected, get the parent first before appending the policy name
                    PolicyPath = SelectedNode.Parent.Text + @"\" + SelectedNode.Text + @"\";
                }
                else if (NodeTag == "AppNode")
                {
                    // Whole app selected
                    PolicyPath = SelectedNode.Text + @"\";
                }
                else
                {
                    MessageBox.Show("Sorry, there's an problem while trying to get your selected IFEO entries.", "[ ERROR ] Unknown type of Selected Policy");
                    return false;
                }

                try
                {
                    // Open the root
                    IFEORoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\", true);

                    IFEORoot.OpenSubKey(NodeTag, true);

                    IFEORoot.Close();

                    IFEORoot = null;

                    IFEORoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\", true);
                }
                catch (System.Security.SecurityException _)
                {
                    MessageBox.Show("Unable to open the IFEO\n" + _.ToString(), "[ ERROR ] OPEN IFEO ROOT Access Denied");
                    return false;
                }

                if (IFEORoot == null)
                {
                    MessageBox.Show("The selected IFEO isn't found in the registry.");
                    return false;
                }


                DialogResult LastConfirmation;
                
                if (BypassSecurityQuestions)
                {
                    LastConfirmation = DialogResult.Yes;
                }
                else
                {
                    string LastConfirmationDialogMessage = "This is the last confirmation, Are you sure you want to delete ";

                    if (NodeTag == "AppPolicyNode")
                    {
                        LastConfirmationDialogMessage += "the : \n    " + SelectedNode.Text + "\nwhich is an IFEO policy of the app :\n    " + SelectedNode.Parent.Text + "\n\n\nNO ONE IN THE WORLD ARE RESPONSIBLE IF THIS APPLICATION CAUSES ANY PROBLEM, BY CONTINUING YOU AGREE NOT TO HELD ANYONE IN THE WORLD RESPONSIBLE FOR THE PROBLEM CAUSED BY THIS APP USAGE.";
                    }
                    else if (NodeTag == "AppNode")
                    {
                        LastConfirmationDialogMessage += "all of the IFEO Policy for :\n    " + SelectedNode.Text + "\n\n\nNO ONE IN THE WORLD ARE RESPONSIBLE IF THIS APPLICATION CAUSES ANY PROBLEM, BY CONTINUING YOU AGREE NOT TO HELD ANYONE IN THE WORLD RESPONSIBLE FOR THE PROBLEM CAUSED BY THIS APP USAGE.";
                    }

                    LastConfirmation = MessageBox.Show(LastConfirmationDialogMessage, "Remove Selected IFEO Last Confirmation Dialog", MessageBoxButtons.YesNoCancel);
                }

                if (LastConfirmation == DialogResult.Yes)
                {
                    // Delete.
                    IFEORoot.DeleteSubKeyTree(PolicyPath);

                    IFEORoot.Flush();

                    RegistryKey DeleteChecker = IFEORoot.OpenSubKey(PolicyPath);
                    if (DeleteChecker != null)
                    {
                        MessageBox.Show("Unable to delete the " + PolicyPath + ", try checking registry permissions or running this app as administrator.", "IFEO EXIST AFTER DELETION ERROR");
                    }
                    else
                    {
                        MessageBox.Show("Sucessfully removed the " + PolicyPath + "\n\n You might need to restart the machine before the changes get applied.", "IFEO GONE DELETED");
                        // Close and flush changes if there is any.
                        IFEORoot.Close();
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Delete Action aborted, no change has been made to your registry.");
                }

                // Close and flush changes if there is any.
                IFEORoot.Close();
                return false;
            }
            else
            {
                // If not OK, then don't do anything.
                return false;
            }
        }

        private void RemoveEverything_Click(object sender, EventArgs e)
        {
            if (DisplayTreeView.Nodes.Count == 0)
            {
                MessageBox.Show("There are nothing to be removed in the display list, no change has been made to your registry.");
                return;
            }

            DialogResult LastConfirmation = MessageBox.Show("You sure you want to remove all entries listed on the display ?\n\n\nNO ONE IN THE WORLD ARE RESPONSIBLE IF THIS APPLICATION CAUSES ANY PROBLEM, BY CONTINUING YOU AGREE NOT TO HELD ANYONE IN THE WORLD RESPONSIBLE FOR THE PROBLEM CAUSED BY THIS APP USAGE.", "Recursive IFEO Remover Last Confirmation Dialog", MessageBoxButtons.YesNoCancel);

            if (LastConfirmation != DialogResult.Yes)
            {
                MessageBox.Show("Recursive Delete Action aborted, no change has been made to your registry.");
                return;
            }
            else
            {
                foreach (TreeNode Nodes in DisplayTreeView.Nodes)
                {
                    DisplayTreeView.SelectedNode = Nodes;
                    if (!RemoveSelectedEntry_Click(true))
                    {
                        // An error occured, break the loop.
                        MessageBox.Show("Unable to delete recursively, try checking registry permissions or running this app as administrator.", "RECURSIVE UPSTREAM ERROR");
                        return;
                    }
                }

                MessageBox.Show("Sucessfully removed the everything.\n\n You might need to restart the machine before the changes get applied.", "IFEO GONE DELETED");
                RefreshTreeDisplay_Click(sender, e);
            }
        }
    }
}
