﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WordGenPreviewTool
{
  /// <summary>
  /// zut class for Windows Form.
  /// </summary>
  public static class zut_wf
  {
    /// <summary>
    /// Remove all selected items in given ListView.
    /// </summary>
    /// <param name="v">ListView to remove item.</param>
    /// <returns>Number of selected items.</returns>
    public static int ListView_RemoveSelected(ListView v)
    {
      int answer = v.SelectedIndices.Count;
      foreach (ListViewItem i in v.SelectedItems)
      {
        v.Items.RemoveAt(i.Index);
      }
      return answer;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool ListView_ContainsItemWithText(ListView v, string text)
    {
      bool answer = false;
      foreach (ListViewItem i in v.Items)
      {
        if (i.Text == text)
        {
          answer = true;
          break;
        }
      }
      return answer;
    }
    public static void ListView_RemoveItemsWithSetofText(ListView v, HashSet<string> s)
    {
      List<ListViewItem> removeList = new List<ListViewItem>();
      foreach (ListViewItem i in v.Items)
        if (s.Contains(i.Text)) removeList.Add(i);
      foreach (ListViewItem i in removeList)
        v.Items.Remove(i);
    }
    public static void ListView_AddTextIfNotContained(ListView v, string text)
    {
      if (!ListView_ContainsItemWithText(v, text)) v.Items.Add(text);
    }
    public static void ListView_AdjustTileSizetoFirstItem(ListView v)
    {
      v.TileSize = new System.Drawing.Size((int)Math.Round(v.CreateGraphics().MeasureString(v.Items[0].Text, v.Font).Width) + v.Items[0].Text.Length * 1 + 4, 28);
      string t = v.Items[v.Items.Count - 1].Text;
      v.Items.RemoveAt(v.Items.Count - 1);
      v.Items.Add(t);
      //v.Refresh();

      //v.PerformLayout();
      //v.ResumeLayout(true);
      //v.Invalidate();
      //v.Update();

    }
    public static string ListView_GenerateStringofItems(ListView l)
    {
      string s = "";
      foreach (ListViewItem i in l.Items)
        s += i.Text + " ";
      return s.Trim();
    }

    public static string ListBox_GetSelectedItemText(ListBox l)
    {
      if (l.SelectedItem != null)
        return l.SelectedItem.ToString();
      return null;
    }

    public static void ListBox_RemoveAllSelectedItems(ListBox l)
    {
      if (l.SelectedIndices.Count == 0) return;
      SortedSet<int> set = new SortedSet<int>();
      foreach (object i in l.SelectedItems)
      {
        set.Add(l.Items.IndexOf(i));
      }
      while (set.Count > 0)
      {
        l.Items.RemoveAt(set.Max);
        set.Remove(set.Max);
      }
    }
    public static bool ListBox_ContainsItemWithText(ListBox l, string s)
    {
      foreach (object i in l.Items)
        if (i.ToString() == s) return true;
      return false;
    }
    public static void ListBox_AddTextIfNotContained(ListBox l, string s)
    {
      if (!ListBox_ContainsItemWithText(l, s)) l.Items.Add(s);
    }
    public static void Clipboard_SetTextIfNotEmpty(string s)
    {
     if (s.Trim().Length>0) Clipboard.SetText(s, TextDataFormat.UnicodeText);
    }
  }
  /// <summary>
  /// zut class of general algorithms.
  /// </summary>
  public class zut
  {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="before">the list before the change</param>
    /// <param name="after">the list after the change</param>
    /// <returns>a Tuple that first element is what's new, second element is what's deleted.</returns>
    public static Tuple<List<T>, List<T>> listDiff<T>(List<T> before, List<T> after)
    {
      List<T> added, deleted;
      added = new List<T>(after);//only what we have now can possible be added
      deleted = new List<T>(before);//only what we've had can be deleted

      foreach (T i in after)
      {
        if (before.Contains(i))
        {
          deleted.Remove(i);//subtract the common ones
          added.Remove(i);
        }
      }

      return new Tuple<List<T>, List<T>>(added, deleted);
    }
  }
}