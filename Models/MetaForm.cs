using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	/// <summary>
	/// Метописание отчетной формы
	/// </summary>
	public partial class MetaForm
	{
		/// <summary>
		/// Код метаописания. Часто его называю кодом формы
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// Намиенование формы. Обычно совпадает с названием мониторинга, без
		/// порядкового номера в начале.
		/// </summary>
		public string Name { get; set; }
	}
}
