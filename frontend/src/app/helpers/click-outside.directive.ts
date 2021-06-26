import { closest } from '@/helpers/dom-helpers';
import { Directive, ElementRef, EventEmitter, OnDestroy, Output } from '@angular/core';

@Directive({
  selector: '[clickOutside]'
})
export class ClickOutsideDirective implements OnDestroy {

  @Output() clickOutside = new EventEmitter<MouseEvent>()
  documentClickListener: (event: MouseEvent) => void

  constructor(
    readonly element: ElementRef
  ) {
    this.documentClickListener = (event: MouseEvent) => {
      if (!event.target || !(event.target instanceof HTMLElement)) {
        return
      }
      const targetIsOutsideElement = closest(event.target, element.nativeElement) == null
      if (targetIsOutsideElement) {
        this.clickOutside.emit(event)
      }
    }
    document.addEventListener('click', this.documentClickListener)
  }

  ngOnDestroy(): void {
    document.removeEventListener('click', this.documentClickListener)
  }

}
