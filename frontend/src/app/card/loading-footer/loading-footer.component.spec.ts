import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingFooterComponent } from './loading-footer.component';

describe('LoadingFooterComponent', () => {
  let component: LoadingFooterComponent;
  let fixture: ComponentFixture<LoadingFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadingFooterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
