using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public enum OrganizationTheme
	{
		DoğumGünü,
		Düğün,
		Nişan,
		EvDaveti,
		Kokteyl,
		Piknik,
		KongreDaveti,
		İftarDaveti
	}

	public class CateringOrder : IEntity<int>
	{
		public int Id { get; set; }
		public OrganizationTheme OrganizationTheme { get; set; }
		public DateTime OrganizationDate { get; set; }
		public DateTime OrganizationTime { get; set; }
		public int NumberOfParticipants { get; set; }
		public string OrganizationAddress { get; set; }
	}
}
