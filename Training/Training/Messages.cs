using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Training
{
    public class Messages
    {
        private static IEnumerable<Message> messages = new List<Message>()
        {
            new Message() { CultureName = "en-GB", Key = "compare_end_date_star_date", Value = "End date must be greater to start date." },
            new Message() { CultureName = "en-GB", Key = "compare_datetime_star_date", Value = "DateTime must be greater to start date."},
            new Message() { CultureName = "en-GB", Key = "compare_current_date_end_date", Value = "Current date must be lesser to end date."},
            new Message() { CultureName = "en-GB", Key = "compare_datetime_end_date", Value = "DateTime must be lesser to end date."},
            new Message() { CultureName = "en-GB", Key = "compare_current_date_datetime", Value = "Current date must be lesser to datetime."},
            new Message() { CultureName = "en-GB", Key = "occurs_once_datetime_null", Value = "Occurs once must indicate the datetime."},
            new Message() { CultureName = "en-GB", Key = "every_must_be_0", Value = "Mustn't indicate every."},
            new Message() { CultureName = "en-GB", Key = "occurs_recurring_datetime", Value = "Occurs recurring mustn’t indicate the datetime."},
            new Message() { CultureName = "en-GB", Key = "every_must_greater_to_0", Value = "Must indicate every." },
            new Message() { CultureName = "en-GB", Key = "occurs_weekly_must_indicate_the_days_of_the_week", Value = "Occurs weekly must indicate the days of the week." },
            new Message() { CultureName = "en-GB", Key = "occurs_monthy_day_must_indicate_the_day_of_the_week", Value = "Occurs monthy day must indicate the day of the month."},
            new Message() { CultureName = "en-GB", Key = "occurs_monthy_day_not_valid", Value = "Occurs monthy day mustn’t be greater to 31."},
            new Message() { CultureName = "en-GB", Key = "occurs_monthy_mustnt_indicate_the_days_of_the_week", Value = "Occurs monthy mustn’t indicate the days of the week."},
            new Message() { CultureName = "en-GB", Key = "occurs_monthy_mustnt_indicate_the_day_of_the_month", Value = "Occurs monthy day mustn’t indicate the day of the month." },
            new Message() { CultureName = "en-GB", Key = "occurs_monthy_must_indicate_the_days_of_the_week", Value = "Occurs monthy must indicate the days of the week." },
            new Message() { CultureName = "en-GB", Key = "must_indicate_occurs_once_at_time", Value = "Must indicate Occurs once at time." },
            new Message() { CultureName = "en-GB", Key = "occurs_every_must_be_0", Value = "Occurs every must be 0." },
            new Message() { CultureName = "en-GB", Key = "must_indicate_starting_at", Value = "Must indicate starting at." },
            new Message() { CultureName = "en-GB", Key = "must_indicate_end_at", Value = "Must indicate end at." },
            new Message() { CultureName = "en-GB", Key = "compare_end_at_starting_at", Value = "End at must be greater to stating at." },
            new Message() { CultureName = "en-GB", Key = "occurs_every_must_be_greater_to_0", Value = "Occurs every must be greater to 0." },
            new Message() { CultureName = "en-GB", Key = "and", Value = " and " },
            new Message() { CultureName = "en-GB", Key = "beetween", Value = " beetween " },
            new Message() { CultureName = "en-GB", Key = "day", Value = " Day " },
            new Message() { CultureName = "en-GB", Key = "on", Value = " on " },
            new Message() { CultureName = "en-GB", Key = "every", Value = " every " },
            new Message() { CultureName = "en-GB", Key = "frecuency_day", Value = "Occurs every day. Schedule will be used on " },
            new Message() { CultureName = "en-GB", Key = "frecuency_once", Value = "Occurs once. Schedule will be used on " },
            new Message() { CultureName = "en-GB", Key = "frecuency_week1", Value = "Occurs every " },
            new Message() { CultureName = "en-GB", Key = "frecuency_week2", Value = " week on " },
            new Message() { CultureName = "en-GB", Key = "starting_on", Value = " starting on " },
            new Message() { CultureName = "en-GB", Key = "until", Value = " until " },
            new Message() { CultureName = "en-US", Key = "compare_end_date_star_date", Value = "End date must be greater to start date." },
            new Message() { CultureName = "en-US", Key = "compare_datetime_star_date", Value = "DateTime must be greater to start date."},
            new Message() { CultureName = "en-US", Key = "compare_current_date_end_date", Value = "Current date must be lesser to end date."},
            new Message() { CultureName = "en-US", Key = "compare_datetime_end_date", Value = "DateTime must be lesser to end date."},
            new Message() { CultureName = "en-US", Key = "compare_current_date_datetime", Value = "Current date must be lesser to datetime."},
            new Message() { CultureName = "en-US", Key = "occurs_once_datetime_null", Value = "Occurs once must indicate the datetime."},
            new Message() { CultureName = "en-US", Key = "every_must_be_0", Value = "Mustn't indicate every."},
            new Message() { CultureName = "en-US", Key = "occurs_recurring_datetime", Value = "Occurs recurring mustn’t indicate the datetime."},
            new Message() { CultureName = "en-US", Key = "every_must_greater_to_0", Value = "Must indicate every." },
            new Message() { CultureName = "en-US", Key = "occurs_weekly_must_indicate_the_days_of_the_week", Value = "Occurs weekly must indicate the days of the week." },
            new Message() { CultureName = "en-US", Key = "occurs_monthy_day_must_indicate_the_day_of_the_week", Value = "Occurs monthy day must indicate the day of the month."},
            new Message() { CultureName = "en-US", Key = "occurs_monthy_day_not_valid", Value = "Occurs monthy day mustn’t be greater to 31."},
            new Message() { CultureName = "en-US", Key = "occurs_monthy_mustnt_indicate_the_days_of_the_week", Value = "Occurs monthy mustn’t indicate the days of the week."},
            new Message() { CultureName = "en-US", Key = "occurs_monthy_mustnt_indicate_the_day_of_the_month", Value = "Occurs monthy day mustn’t indicate the day of the month." },
            new Message() { CultureName = "en-US", Key = "occurs_monthy_must_indicate_the_days_of_the_week", Value = "Occurs monthy must indicate the days of the week." },
            new Message() { CultureName = "en-US", Key = "must_indicate_occurs_once_at_time", Value = "Must indicate Occurs once at time." },
            new Message() { CultureName = "en-US", Key = "occurs_every_must_be_0", Value = "Occurs every must be 0." },
            new Message() { CultureName = "en-US", Key = "must_indicate_starting_at", Value = "Must indicate starting at." },
            new Message() { CultureName = "en-US", Key = "must_indicate_end_at", Value = "Must indicate end at." },
            new Message() { CultureName = "en-US", Key = "compare_end_at_starting_at", Value = "End at must be greater to stating at." },
            new Message() { CultureName = "en-US", Key = "occurs_every_must_be_greater_to_0", Value = "Occurs every must be greater to 0." },
            new Message() { CultureName = "en-US", Key = "and", Value = " and " },
            new Message() { CultureName = "en-US", Key = "beetween", Value = " beetween " },
            new Message() { CultureName = "en-US", Key = "day", Value = " Day " },
            new Message() { CultureName = "en-US", Key = "every", Value = " every " },
            new Message() { CultureName = "en-US", Key = "frecuency_day", Value = "Occurs every day. Schedule will be used on " },
            new Message() { CultureName = "en-US", Key = "frecuency_once", Value = "Occurs once. Schedule will be used on " },
            new Message() { CultureName = "en-US", Key = "frecuency_week1", Value = "Occurs every " },
            new Message() { CultureName = "en-US", Key = "frecuency_week2", Value = " week on " },
            new Message() { CultureName = "en-US", Key = "on", Value = " on " },
            new Message() { CultureName = "en-US", Key = "starting_on", Value = " starting on " },
            new Message() { CultureName = "en-US", Key = "until", Value = " until " },
            new Message() { CultureName = "es-ES", Key = "compare_end_date_star_date", Value = "Fecha fin debe ser mayor a fecha inicio." },
            new Message() { CultureName = "es-ES", Key = "compare_datetime_star_date", Value = "Fecha y hora debe ser mayor a fecha inicio."},
            new Message() { CultureName = "es-ES", Key = "compare_current_date_end_date", Value = "Fecha actual debe ser menor a fecha fin."},
            new Message() { CultureName = "es-ES", Key = "compare_datetime_end_date", Value = "Fecha y hora debe ser menor a fecha fin."},
            new Message() { CultureName = "es-ES", Key = "compare_current_date_datetime", Value = "Fecha actual debe ser menor a fecha y hora."},
            new Message() { CultureName = "es-ES", Key = "occurs_once_datetime_null", Value = "Para una unica ejecución debe indicar fecha y hora."},
            new Message() { CultureName = "es-ES", Key = "every_must_be_0", Value = "No debe indicar cada."},
            new Message() { CultureName = "es-ES", Key = "occurs_recurring_datetime", Value = "No debe indicar fecha y hora recurrente."},
            new Message() { CultureName = "es-ES", Key = "every_must_greater_to_0", Value = "Debe indicar cada" },
            new Message() { CultureName = "es-ES", Key = "occurs_weekly_must_indicate_the_days_of_the_week", Value = "Ocurrencia semanal debe indicar los días de la semana." },
            new Message() { CultureName = "es-ES", Key = "occurs_monthy_day_must_indicate_the_day_of_the_week", Value = "Ocurrencia mensual día debe indicar el día del mes."},
            new Message() { CultureName = "es-ES", Key = "occurs_monthy_day_not_valid", Value = "Ocurrencia mensual día no debe ser mayo a 31."},
            new Message() { CultureName = "es-ES", Key = "occurs_monthy_mustnt_indicate_the_days_of_the_week", Value = "Ocurrencia mensual no debe indicarlos días de la semana."},
            new Message() { CultureName = "es-ES", Key = "occurs_monthy_mustnt_indicate_the_day_of_the_month", Value = "Ocurrencia mensual día no debe indicar el día del mes." },
            new Message() { CultureName = "es-ES", Key = "occurs_monthy_must_indicate_the_days_of_the_week", Value = "Ocurrencia mensual debe indicar los días de la semana." },
            new Message() { CultureName = "es-ES", Key = "must_indicate_occurs_once_at_time", Value = "Debe indicar ocurrencia cada en hora." },
            new Message() { CultureName = "es-ES", Key = "occurs_every_must_be_0", Value = "Ocurrencia cada no debe ser indicada." },
            new Message() { CultureName = "es-ES", Key = "must_indicate_starting_at", Value = "Debe indicar iniciado en." },
            new Message() { CultureName = "es-ES", Key = "must_indicate_end_at", Value = "Debe indicar fin en." },
            new Message() { CultureName = "es-ES", Key = "compare_end_at_starting_at", Value = "Fin en debe ser mayor a iniciado en." },
            new Message() { CultureName = "es-ES", Key = "occurs_every_must_be_greater_to_0", Value = "Ocurrencia cada debe ser indicado."},
            new Message() { CultureName = "es-ES", Key = "and", Value = " y " },
            new Message() { CultureName = "es-ES", Key = "beetween", Value = " entre " },
            new Message() { CultureName = "es-ES", Key = "day", Value = " Día " },
            new Message() { CultureName = "es-ES", Key = "every", Value = " cada " },
            new Message() { CultureName = "es-ES", Key = "frecuency_day", Value = "Cada día. Planificador sera usade en " },
            new Message() { CultureName = "es-ES", Key = "frecuency_once", Value = "Una vez. Planificador sera usado en " },
            new Message() { CultureName = "es-ES", Key = "frecuency_week1", Value = "Ocurre cada " },
            new Message() { CultureName = "es-ES", Key = "frecuency_week2", Value = " semana en " },
            new Message() { CultureName = "es-ES", Key = "starting_on", Value = " comenzado en " },
            new Message() { CultureName = "es-ES", Key = "until", Value = " hasta " }
        };

        public static Message Get(string key, CultureInfo culture)
        {
            var messages = (from message in Messages.messages
                            where message.Key == key && message.CultureName == culture.Name
                            select message);

            if (messages.Count() == 0)
            {
                throw new ScheduleException("Notification not available.");
            }

            return messages.First();
        }
    }
}
