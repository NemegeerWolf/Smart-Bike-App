package crc647e2fed97a0fc2e89;


public class Adapter
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.bluetooth.BluetoothAdapter.LeScanCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLeScan:(Landroid/bluetooth/BluetoothDevice;I[B)V:GetOnLeScan_Landroid_bluetooth_BluetoothDevice_IarrayBHandler:Android.Bluetooth.BluetoothAdapter/ILeScanCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BluetoothLE.Droid.Adapter, BluetoothLE.Droid", Adapter.class, __md_methods);
	}


	public Adapter ()
	{
		super ();
		if (getClass () == Adapter.class)
			mono.android.TypeManager.Activate ("BluetoothLE.Droid.Adapter, BluetoothLE.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2)
	{
		n_onLeScan (p0, p1, p2);
	}

	private native void n_onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
