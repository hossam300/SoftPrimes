import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validator, ValidatorFn, FormControl } from '@angular/forms';

function validateFactory() {
  return (c: AbstractControl) => {
    const isValid = validateRequired(c.value);
    if (isValid) { return null; } else {
      return { valid: false, message: '*', };
    }
  };
}

function validateRequired(text) {
  return text ? (text.toString().trim().length > 0 ? true : false) : false;
}


@Directive({
  selector: '[app-required][ngModel]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: RequiredDirective, multi: true }
  ]
})
export class RequiredDirective {
  validator: ValidatorFn;

  constructor() {
    this.validator = validateFactory();
  }

  validate(c: FormControl) {
    return this.validator(c);
  }
}
