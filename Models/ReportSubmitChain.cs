using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Цепочка сдачи отчетности. Специальный объект, с иерархической структорой
	/// содержащий все организации, которым нужно будет сдавать очеты из пекта очтетных форм
	/// компонента, за которым данная цепочка закерпляется. Чаще всего родителем выступает 
	/// элемент-организация Приморский край, с потомками в виде МО. Есть, впрочем, исключения
	/// </summary>
	public partial class ReportSubmitChain
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public ReportSubmitChain()
		{
			ReportPeriodComponents = new HashSet<ReportPeriodComponent>();

			ChainElements = new HashSet<ReportSubmitChainElement>();
		}

		/// <summary>
		/// Код цепочки (уникальный). Обычно идет в формате 0123, но в случае, когда
		/// у мониторинга менялся набор сдаваемых его организаций, для цепочки солздаются
		/// копии с кодами формат "0123 01" (без кавычек)
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Признак активности цепочки
		/// </summary>
		public bool? IsDisabled { get; set; }
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// Наименование цепочки. Чаще всего сопадает с названием отчета. Так же содержит фамилию эксперта
		/// отчета.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Навигационное свойство со списком компонентов отчетных периодов, к оторым цепочка прикреплена
		/// </summary>
		public virtual ICollection<ReportPeriodComponent> ReportPeriodComponents { get; set; }
		/// <summary>
		/// Навигационное свойство со списком элементов данной цепочки сдачи отчетности
		/// </summary>
		public virtual ICollection<ReportSubmitChainElement> ChainElements { get; set; }
	}
}
