NetSdoGeometry
==============

Fork of the NetSdoGeometry project as can be found [here on CodePlex](http://tf-net.googlecode.com/files/NetSdoGeometry.zip)

I applied StyleCop rules to the files and restructured some of the classes into separate files.

The code snippet below shows how to use it.

```C#
OracleDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    NetSdoGeometry.SdoGeometry geom = reader["geometry"] as NetSdoGeometry.SdoGeometry;
}
```
