import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DropMenueComponent } from './drop-menue.component';

describe('DropMenueComponent', () => {
  let component: DropMenueComponent;
  let fixture: ComponentFixture<DropMenueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DropMenueComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DropMenueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
