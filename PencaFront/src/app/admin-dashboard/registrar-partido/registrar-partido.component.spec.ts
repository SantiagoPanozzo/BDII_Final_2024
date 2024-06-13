import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarPartidoComponent } from './registrar-partido.component';

describe('RegistrarPartidoComponent', () => {
  let component: RegistrarPartidoComponent;
  let fixture: ComponentFixture<RegistrarPartidoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrarPartidoComponent]
    });
    fixture = TestBed.createComponent(RegistrarPartidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
