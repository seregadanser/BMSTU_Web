import { Component , Input, Output, EventEmitter, OnChanges, SimpleChanges} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../button/button.component';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
export class PaginationComponent {
  @Input() current: number = 0
  @Input() total: number = 0
  @Output() goTo: EventEmitter<number> = new EventEmitter<number>()
  @Output() next: EventEmitter<number> = new EventEmitter<number>()
  @Output() previous: EventEmitter<number> = new EventEmitter<number>()
  @Input()
  top = "100px";
  @Input()
  left = "100px";

  public pages: number[] = []
  public onGoTo(page: number): void {
    this.goTo.emit(page)
  }
  public onNext(): void {
    if(this.current == this.total)
    return;
    this.next.emit(this.current)
  }
  public onPrevious(): void {
    if(this.current == 1)
    return;
    this.previous.next(this.current)
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (
      (changes['current'] && changes['current'].currentValue) ||
      (changes['total'] && changes['total'].currentValue)
    ) {
      this.pages = this.getPages(this.current, this.total)
    }
  }

  private getPages(current: number, total: number): number[] {
    if (total <= 7) {
      return [...Array(total).keys()].map(x => ++x)
    }

    if (current > 5) {
      if (current >= total - 4) {
        return [1, -1, total - 4, total - 3, total - 2, total - 1, total]
      } else {
        return [1, -1, current - 1, current, current + 1, -1, total]
      }
    }

    return [1, 2, 3, 4, 5, -1, total]
  }


}
