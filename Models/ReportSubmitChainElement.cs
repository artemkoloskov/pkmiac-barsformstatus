using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Элемент цепочки сдачи отчетности. Явлется контейнером для организации,
	/// которой нужно будет сдавать отчет, закрепленный за компонентом отчетного периода, к которому
	/// прикреплена цепочка.
	/// </summary>
	public partial class ReportSubmitChainElement
	{
		/// <summary>
		/// Конструткор
		/// </summary>
		public ReportSubmitChainElement()
		{
			ChildrenElements = new HashSet<ReportSubmitChainElement>();
		}

		/// <summary>
		/// Уникальный идентификатор организации
		/// </summary>
		public Guid? OrganizationId { get; set; }
		/// <summary>
		/// Тип элемента:
		///  ЦентральныйОфис = 0,
		///  Офис = 1,
		///  Абонент = 2,
		///  ПассивныйАбонент = 3
		/// </summary>
		public byte Type { get; set; }
		/// <summary>
		/// Уникальный идентификатор родительского элемента при наличии
		/// </summary>
		public Guid? ParentId { get; set; }
		/// <summary>
		/// Уникальный идентификатор цепочки
		/// </summary>
		public Guid? ReportSubmitChainId { get; set; }
		/// <summary>
		/// Уникальный идентифкатор элемента цепочки
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// Намиенование элемента, обычно свопадает с наименованием организации
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Навигационное свойство с родительским элементом, при наличии
		/// </summary>
		[JsonIgnore]
		public virtual ReportSubmitChainElement ParentChainElement { get; set; }
		/// <summary>
		/// Навигационное свойство с потомками, при наличии
		/// </summary>
		[JsonIgnore]
		public virtual ICollection<ReportSubmitChainElement> ChildrenElements { get; set; }
		/// <summary>
		/// Навигационное свойство с организацией, которая явлется данным элементом
		/// </summary>
		public virtual Organization Organization { get; set; }
		/// <summary>
		/// Навигационное свойство с цепочкой, элемнтом которй явлется данный элемент
		/// </summary>
		public virtual ReportSubmitChain ReportSubmitChain { get; set; }
	}
}
