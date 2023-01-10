export interface AssistanceResponseDto {
  Successful: boolean
  ErrorMessage: string
  AssistanceType: AssistanceType
}

export enum AssistanceType {
  Asistencia,
  Retardo,
  Falta,
}
