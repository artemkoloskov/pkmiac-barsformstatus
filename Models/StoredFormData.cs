using System;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Хранимые данные формы - объект, содержащий аттрибуты конкретного отчета, заполненого конкретной
	/// организацие, в конкретном компоненте отчетного периода
	/// </summary>
	public partial class StoredFormData
	{
		/// <summary>
		/// Уникальный идентификатор организации, которая заполняет данную форму
		/// </summary>
		public Guid? OrganizationId { get; set; }
		/// <summary>
		/// Код метаописания отчетной формы
		/// </summary>
		public string MetaFormCode { get; set; }
		/// <summary>
		/// СТАТУС ЗАПОЛНЕНИЯ ФОРМЫ:
		///  Пусто = 0,
		///  Черновик = 1,
		///  Заполнено = 2,
		///  Проверено = 3,
		///  Экспертиза = 4,
		///  Утверждено = 5
		/// </summary>
		public byte StatusNumber { get; set; }
		/// <summary>
		/// Статус проверки внутриформенных увязок:
		///  НеПроверено = 0,
		///  ИмеютсяОшибки = 1,
		///  ИмеютсяПредупреждения = 2,
		///  Проверено = 3
		/// </summary>
		public byte InternalConstraintsStatusNumber { get; set; }
		/// <summary>
		/// Статус проверки межформенных увязок:
		///  НеПроверено = 0,
		///  ИмеютсяОшибки = 1,
		///  ИмеютсяПредупреждения = 2,
		///  Проверено = 3
		/// </summary>
		public byte ExternalConstraintsStatusNumber { get; set; }
		/// <summary>
		/// Уникальный идентификатор компонента отчетного периода, в котором заполнена данная форма
		/// </summary>
		public Guid? ReportPeriodComponentId { get; set; }
		/// <summary>
		/// Тип элемента цепочки сдачи отчетных форм, который заполнял форму
		/// </summary>
		public byte SubmitChainElementType { get; set; }
		/// <summary>
		/// Статус экспертизы:
		///  НеПройдена = 0,
		///  ИмеютсяОшибки = 1,
		///  Пройдена = 2
		/// </summary>
		public byte ExpertStatusNumber { get; set; }
		/// <summary>
		/// Уникальный идентифкатор
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// Навигационное свойство с компонентом, в котором была заполнена форма
		/// </summary>
		public virtual ReportPeriodComponent ReportPeriodComponent { get; set; }
		/// <summary>
		/// Навигационное свойство с организацией, которая заполнила форму
		/// </summary>
		public virtual Organization Organization { get; set; }
	}
}
