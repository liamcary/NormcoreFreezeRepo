[RealtimeModel]
public partial class LargeModel
{
	[RealtimeProperty(1, false)] byte[] _data = new byte[128 * 128 * 3];
}
