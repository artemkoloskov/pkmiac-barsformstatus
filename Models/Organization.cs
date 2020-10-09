using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Организация. В случае МИАЦ - МО
	/// </summary>
	public partial class Organization
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public Organization()
		{
			StoredFormData = new HashSet<StoredFormData>();

			ChainElements = new HashSet<ReportSubmitChainElement>();
		}

		/// <summary>
		/// Наименование организации
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Код организации в формате 33322211100
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Регион расположения
		/// </summary>
		public string Region { get; set; }
		/// <summary>
		/// Тип населенного пункта (г., с. и т.п.)
		/// </summary>
		public string LocalityType { get; set; }
		/// <summary>
		/// Наименование населенного пункта
		/// </summary>
		public string LocalityName { get; set; }
		/// <summary>
		/// Признак того, что организация ликвидирована
		/// </summary>
		public bool? Terminated { get; set; }
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// Навигационное свойство со списокм хранимых данных форм,
		/// заполненяемых организацией
		/// </summary>
		public virtual ICollection<StoredFormData> StoredFormData { get; set; }
		/// <summary>
		/// Навигационное свойство со списком элемнтов цепочек, в которые входит
		/// оргнанизация
		/// </summary>
		public virtual ICollection<ReportSubmitChainElement> ChainElements { get; set; }
	}
}
