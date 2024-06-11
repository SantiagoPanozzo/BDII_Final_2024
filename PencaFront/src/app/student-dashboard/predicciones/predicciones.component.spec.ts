import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrediccionesComponent } from './predicciones.component';

describe('PrediccionesComponent', () => {
  let component: PrediccionesComponent;
  let fixture: ComponentFixture<PrediccionesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PrediccionesComponent]
    });
    fixture = TestBed.createComponent(PrediccionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
