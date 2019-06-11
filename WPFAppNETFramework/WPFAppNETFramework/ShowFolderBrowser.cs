
using (FolderBrowserDialog dlg = new FolderBrowserDialog())
{
    dlg.Description = "Select a folder";
    if (dlg.ShowDialog() == DialogResult.OK)
    {
        MessageBox.Show("You selected: " + dlg.SelectedPath);
    }
}