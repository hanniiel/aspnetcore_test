export interface AssistanceForRegistrationDto {
  IdUser?: string //set this by session/server side
  FechaInicialTurno: Date //serverside would be better
  FechaFinalTurno: Date //same that before
  IdSchedule?: number //set this on user or user array if multiple
  IdWorkplace: number //set this by user nearest location or attached to user depending on org
}
