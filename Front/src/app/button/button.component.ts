import { Component, Input, Output, EventEmitter} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  @Input()
  buttonText = 'Войти';
  @Input()
  svgPath = '../assets/24.svg';

  @Input()
  top = "100px";
  @Input()
  left = "100px";

  @Input()
  set isImageVisible(value: boolean | string) {
    this._isImageVisible = value !== 'false';
  }
  get isImageVisible(): boolean {
    return this._isImageVisible;
  }

  @Input()
  set isTextVisible(value: boolean | string) {
    this._isTextVisible = value !== 'false';
  }
  get isTextVisible(): boolean {
    return this._isTextVisible;
  }

  @Output() btnClick = new EventEmitter();

  onClick() {
		this.btnClick.emit();
	}
  private _isImageVisible = true;
  private _isTextVisible = true;




}
