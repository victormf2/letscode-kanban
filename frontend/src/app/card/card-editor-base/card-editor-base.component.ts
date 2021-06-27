import { Component, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ControlValueAccessor, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { CardValue } from './card-value';

@Component({
  selector: 'app-card-editor-base',
  templateUrl: './card-editor-base.component.html',
  styleUrls: ['./card-editor-base.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CardEditorBaseComponent),
    multi: true,
  }],
})
export class CardEditorBaseComponent implements OnInit, ControlValueAccessor {

  @Output('save') onSave = new EventEmitter<CardValue> ()
  @Output('cancel') onCancel = new EventEmitter()

  cardForm!: FormGroup
  onChange: any
  onTouched: any
  isSaving: boolean
  
  constructor() {
    this.isSaving = false
  }
  writeValue(obj: any): void {
    this.cardForm.setValue(obj)
  }
  registerOnChange(fn: any): void {
    this.onChange = fn
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn
  }
  setDisabledState(isDisabled: boolean): void {
    if (isDisabled) {
      this.cardForm.disable()
    } else {
      this.cardForm.enable()
      this.isSaving = false
    }
  }

  ngOnInit(): void {
    this.cardForm = new FormGroup({
      title: new FormControl('', [
        Validators.required,
        Validators.maxLength(50)
      ]),
      content: new FormControl('', [
        Validators.required
      ]),
    })
  }

  save() {
    this.isSaving = true
    this.onSave.emit(this.cardForm.value)
  }

  cancel() {
    this.onCancel.emit()
  }


}
