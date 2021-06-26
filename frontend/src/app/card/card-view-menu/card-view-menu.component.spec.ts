import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardViewMenuComponent } from './card-view-menu.component';

describe('CardViewMenuComponent', () => {
  let component: CardViewMenuComponent;
  let fixture: ComponentFixture<CardViewMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardViewMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardViewMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
