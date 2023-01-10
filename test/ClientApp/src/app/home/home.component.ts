import { Component, OnInit } from '@angular/core'
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router'
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AssistanceForRegistrationDto } from '../__interfaces/assistance/AssistanceForRegistrationDto';
import { AssistanceResponseDto, AssistanceType } from '../__interfaces/response/AssistanceResponseDto';
import {
  AssistanceService
} from '../shared/services/assistance.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  assistanceForm: FormGroup
  public errorMessage: string = ''
  public showError: boolean
  public resMessage: string = ''
  public showResponse: boolean
 

  constructor(private assService: AssistanceService) { }

  ngOnInit(): void {
    this.assistanceForm = new FormGroup({
      fechaTurnoInicial: new FormControl('', [Validators.required]),
      fechaFinalTurno: new FormControl('', [Validators.required]),
      workplaceId: new FormControl('', [Validators.required]),
    })
  }

  public validateControl = (controlName: string) => {
    return this.assistanceForm?.get(controlName)?.invalid && this.assistanceForm?.get(controlName)?.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.assistanceForm?.get(controlName)?.hasError(errorName)
  }

  public assistance = (assistanceFormValue) => {
    this.showError = false;
    const formValues = { ...assistanceFormValue }

    const assistance: AssistanceForRegistrationDto = {
      FechaInicialTurno: formValues.fechaTurnoInicial,//serverside'd be better
      FechaFinalTurno: formValues.fechaFinalTurno,//servrside'd be better
      IdWorkplace: formValues.workplaceId,//set by location
    }

    this.assService.assistance('api/assistance', assistance)
      .subscribe({
        next: (res: AssistanceResponseDto) => {
          this.showResponse = true
          this.resMessage = res.AssistanceType.toString()
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error
          this.showError = true
        }
      })

    
  }
}

