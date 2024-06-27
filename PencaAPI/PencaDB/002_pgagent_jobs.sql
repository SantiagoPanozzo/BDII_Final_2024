-- Crear la función de notificaciones
CREATE OR REPLACE FUNCTION generar_notificaciones()
RETURNS void LANGUAGE plpgsql AS $$
DECLARE
    partido RECORD;
    alumnoCedula int;
BEGIN
    -- Bucle a través de todos los partidos de mañana
    FOR partido IN 
       select * 
       from Partido 
       where Fecha > CURRENT_DATE + INTERVAL '1 day' 
       and Fecha < CURRENT_DATE + INTERVAL '2 days'
    LOOP
        -- Bucle a través de todos los alumnos sin predicción para el partido
        FOR alumnoCedula IN 
            select a.Cedula
            from Alumno a
            left join Prediccion p 
            ON a.Cedula = p.Cedula 
            and p.Fecha_partido = partido.Fecha
            and p.Equipo_E1 = partido.Equipo_E1
            and p.Equipo_E2 = partido.Equipo_E2
            WHERE p.cedula IS NULL
        LOOP
            -- Crear notificación
            INSERT INTO Notificacion (AlumnoCedula, Mensaje)
            VALUES (
                alumnoCedula, 
                'Recuerda ingresar tu predicción para el partido ' || partido.Equipo_E1 || ' vs ' || partido.Equipo_E2 || ' del ' || partido.Fecha
            );
        END LOOP;
    END LOOP;
END;
$$;

-- Crear el job de pgAgent para ejecutar la función diariamente
INSERT INTO pgagent.pga_job (
    jobid, jobjclid, jobname, jobdesc, jobhostagent, jobcreated, jobenabled
)
VALUES (
    DEFAULT, 1, 'Generar Notificaciones', 'Genera notificaciones para partidos de mañana', '', NOW() + INTERVAL '2 minutes', true
);

-- Agregar un paso al job
INSERT INTO pgagent.pga_jobstep (
    jstjobid, jstname, jstdesc, jstenabled, jstkind, jstcode
)
VALUES (
    (SELECT jobid FROM pgagent.pga_job WHERE jobname = 'Generar Notificaciones'),
    'Paso 1', 'Ejecutar función de notificaciones', true, 'b',
    'psql -U postgres -d pencadb -c "SELECT generar_notificaciones();"'
);

-- Programar el job para que se ejecute cada 5 minutos
INSERT INTO pgagent.pga_schedule (
    jscjobid, jscname, jscdesc, jscenabled, jscstart, jscminutes, jscweekdays
)
VALUES (
    (SELECT jobid FROM pgagent.pga_job WHERE jobname = 'Generar Notificaciones'),
    'Cada 5 minutos', 'Ejecutar cada 5 minutos', true,
    NOW() + INTERVAL '2 minutes',
    '{ true, false, false, false, false, false, false, false, false, false,
        true, false, false, false, false, true, false, false, false, false,
        true, false, false, false, false, true, false, false, false, false,
        true, false, false, false, false, true, false, false, false, false,
        true, false, false, false, false, true, false, false, false, false,
        true, false, false, false, false, true, false, false, false, false}', -- Minutos en los que se ejecutará el job (cada 5 minutos)
    '{true, true, true, true, true, true, true}' -- Todos los días de la semana
);