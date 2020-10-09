using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Пакет отчетных форм. В МИАЦ чаще всего пакет соджержит только одну конкретную форму.
	/// Пакет является главной составляющей компонента отчетного периода, он указывает 
	/// компоененту какие формы нужно заполнять организации в данном компоненте отчетного
	/// периода
	/// </summary>
	public partial class FormBundle
	{
		/// <summary>
		/// конструктор
		/// </summary>
		public FormBundle()
		{
			ReportPeriodComponents = new HashSet<ReportPeriodComponent>();
		}

		/// <summary>
		/// Код пакета отчетных форм. Обычно совпадает с кодом метаописания формы, которая
		/// входит в пакет
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Признак того, акивен ли пакет или нет. Если пакет не активен - отчеты входящие в него
		/// не будут заполняться организацией
		/// </summary>
		public bool? IsDisabled { get; set; }
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// Наименование пакета, обычно совпадает с кодом
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Навигационное свойство со списокм компонентов отчетного периода, использующих
		/// данный пакет
		/// </summary>
		public virtual ICollection<ReportPeriodComponent> ReportPeriodComponents { get; set; }
	}
}
