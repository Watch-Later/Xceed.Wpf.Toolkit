﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;
using System.Windows;

namespace Xceed.Wpf.DataGrid
{
  internal class ItemsSourceChangeCompletedEventManager : WeakEventManager
  {
    private ItemsSourceChangeCompletedEventManager()
    {
    }

    public static void AddListener( DataGridControl source, IWeakEventListener listener )
    {
      CurrentManager.ProtectedAddListener( source, listener );
    }

    public static void RemoveListener( DataGridControl source, IWeakEventListener listener )
    {
      CurrentManager.ProtectedRemoveListener( source, listener );
    }

    protected override void StartListening( object source )
    {
      DataGridControl dataGridControl = ( DataGridControl )source;
      dataGridControl.ItemsSourceChangeCompleted += new EventHandler( this.OnItemsSourceChanged );
    }

    protected override void StopListening( object source )
    {
      DataGridControl dataGridControl = ( DataGridControl )source;
      dataGridControl.ItemsSourceChangeCompleted -= new EventHandler( this.OnItemsSourceChanged );
    }

    private static ItemsSourceChangeCompletedEventManager CurrentManager
    {
      get
      {
        Type managerType = typeof( ItemsSourceChangeCompletedEventManager );
        ItemsSourceChangeCompletedEventManager currentManager = ( ItemsSourceChangeCompletedEventManager )WeakEventManager.GetCurrentManager( managerType );

        if( currentManager == null )
        {
          currentManager = new ItemsSourceChangeCompletedEventManager();
          WeakEventManager.SetCurrentManager( managerType, currentManager );
        }

        return currentManager;
      }
    }

    private void OnItemsSourceChanged( object sender, EventArgs args )
    {
      this.DeliverEvent( sender, args );
    }
  }
}