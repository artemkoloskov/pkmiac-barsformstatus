using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Компонет отчетного периода. Для разных очетов компонент отчетного периода может иметь разное
	/// значение: 
	///  1. для месячных, квартальных и полугодовых отчетов: каждому месяцу (кварталу/полугодию)
	///  года выделяется отдельный отчетный период, в котором каждый компонент - 
	///  это контейнер для пакета отчетных форм, которые нужно заполнять организации
	///  2. для ежедневных и еженедельных - обратная ситуация: каждому отчету выделяется отдельный отчетный период,
	///  в котором каждый компонент - это отчет за определнный день/неделю
	///  3. для запросов: каждому год для запросов выделен отдельный отчетный период,
	///  в котором кадый компонент - это отдельный запрос.
	///  4. у Нац проектов своя структура, консультация с Ефремовым Д. В.
	/// </summary>
	public partial class ReportPeriodComponent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public ReportPeriodComponent()
		{
			StoredFormData = new HashSet<StoredFormData>();
		}

		/// <summary>
		/// Код компонента - обычно это порядковый номер компонента в отчетном периоде
		/// в форате 001, 002 и т.д.
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Уникальный идентификатор пакета форм, который содержится в данном компоненте периода
		/// </summary>
		public Guid? FormBundleId { get; set; }
		/// <summary>
		/// Уникальный идентификатор цепочки сдачи отчетности, которая сожержит организации,
		/// которым нужно заполнить формы, содержащиеся в пакете форм данногшо компонента
		/// </summary>
		public Guid? ReportSubmitChainId { get; set; }
		/// <summary>
		/// Уникальный идентификатор отчетного периода, к котором относится компонент отчетного периода
		/// </summary>
		public Guid? ReportPeriodId { get; set; }
		/// <summary>
		/// Группа эквивалентности. уточнить у Ефремова Д. В. назначение. У еженедельных и ежедневных отчетов
		/// совпадает с наименованием компоента
		/// </summary>
		public string EqualityGroup { get; set; }
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// Наименование компонента отчетного периода. Имеет разное значение:
		///  1. У месячных, квартальных, полугодовых содержит описание мониторинга, который заполняется
		///  организацие в данном компоненте
		///  2. У ежедневных, еженедельных содержит порядковый номер компонента в периоде (совпадаетс кодом
		///  компонента) и период за который в компоненте содержится отчет, вида: "05. на 15.08.2020" и т.п
		///  3. у запросов содержит Номер запроса и описание его сути, вида: "З-802. Отчет такой-то"
		///  4. У нац. проектов своя схема - уточнить у Ефремова Д. В.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Признак того, что компонент активен. Неактивный компонент не будет виден организации,
		/// его отчеты не будут заполнены
		/// </summary>
		public bool? IsDisabled { get; set; }

		/// <summary>
		/// Навигационное свойство с цепочкой сдачи отчетности, которая сожержит организации,
		/// которым нужно заполнить формы, содержащиеся в пакете форм данногшо компонента 
		/// </summary>
		public virtual ReportSubmitChain ReportSubmitChain { get; set; }
		/// <summary>
		/// Навигационное свойство с отчетным периодом, к которому относится компонент отчетного периода
		/// </summary>
		public virtual ReportPeriod ReportPeriod { get; set; }
		/// <summary>
		/// Навигационное свойство с пакетом форм, который содержится в данном компоненте периода
		/// </summary>
		public virtual FormBundle FormsBundle { get; set; }
		/// <summary>
		/// Навигационное свойство со списком хранимых данных форм, заполняемых организациями, в которых
		/// содержится информация о статусе заполнения форм
		/// </summary>
		public virtual ICollection<StoredFormData> StoredFormData { get; set; }
	}
}
