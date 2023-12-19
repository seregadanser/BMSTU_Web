import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css'
})
export class InputComponent {

  @Input()
  svgPath:string = '../assets/24_user.svg';

  @Input()
  placeHolder:string = "Введите текст";

  @Input()
  set showImage(value: boolean | string) {
    this._showImage = value !== 'false';
  }
  get showImage(): boolean {
    return this._showImage ;
  }

  get Text():string
  {
    return this.inputValue;
  }

  @Output() onChange:EventEmitter<string> = new EventEmitter<string>();
  onInputChange() {
      this.onChange.emit(this.inputValue);
  }


 public  inputValue: string = '';

  private _showImage:boolean = true;

  @Output() onEnter:EventEmitter<string> = new EventEmitter<string>();

  onEnterKey() {
    this.onEnter.emit(this.inputValue);
  }
}
