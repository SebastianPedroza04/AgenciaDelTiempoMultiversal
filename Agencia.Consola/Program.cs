using System;
using Agencia.Core;

namespace Agencia.Consola
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("======================================================");
            Console.WriteLine("   A G E N C I A   D E L   T I E M P O   M U L T I V E R S A L");
            Console.WriteLine("======================================================");
            Console.WriteLine("Advertencia:");
            Console.WriteLine("- Las líneas temporales listadas a continuación son clasificadas.");
            Console.WriteLine("- Saltar entre universos altera derechos de autor, canon oficial,");
            Console.WriteLine("  y posiblemente tu estabilidad emocional.");
            Console.WriteLine("------------------------------------------------------");
            Console.Write("Identifíquese, agente: ");
            string nombreAgente = Console.ReadLine();
            Console.WriteLine();

            // Crear multiverso
            Multiverso m = new Multiverso();

            // === CREACIÓN DE UNIVERSOS (36+)
            Universo u0 = m.CrearUniverso(
                $"Universo 0: Línea Base. {nombreAgente} despierta en una sala blanca estilo laboratorio futurista. Pantallas holográficas muestran miles de realidades. " +
                "Se escucha una voz robótica tipo GLaDOS diciendo: \"Bienvenido, insecto emocional.\"",
                15
            );

            Universo u1 = m.CrearUniverso(
                "Universo 1: Tierra-616-¿? versión low budget. Los Vengadores existen, pero tienen que usar transporte público. Hulk hace fila en el SITP con tapabocas.",
                40
            );

            Universo u2 = m.CrearUniverso(
                "Universo 2: Gotham pero con presupuesto de sitcom. Batman tiene que compartir apartamento con el Joker porque la renta en la ciudad está absurda. Alfred hace arepas.",
                55
            );

            Universo u3 = m.CrearUniverso(
                "Universo 3: Shōnen Prime. Todo funciona con 'el poder de la amistad'. Cualquier discusión se resuelve gritando el nombre de tu técnica definitiva durante 4 minutos.",
                70
            );

            Universo u4 = m.CrearUniverso(
                "Universo 4: Mundo estilo Pokémon, pero los Pokémon son electrodomésticos. Una licuadora tipo dragón nivel 72 intenta devorarte el brazo para demostrar afecto.",
                45
            );

            Universo u5 = m.CrearUniverso(
                "Universo 5: Planeta Espada Láser. Claramente inspirado en Star Wars, pero legalmente distinto. Aquí 'la Fuerza' se llama 'Energía Éticamente Ambigua™'.",
                65
            );

            Universo u6 = m.CrearUniverso(
                "Universo 6: Reino Disney Clásico. Todo es musical. Nadie puede simplemente hablar normal. Hasta las ratas cantan armonías a 3 voces y bailan tap.",
                30
            );

            Universo u7 = m.CrearUniverso(
                "Universo 7: Pixar Feelings. Todos los objetos tienen sentimientos. La silla donde te sientas hace terapia después porque piensa que la odiaste.",
                50
            );

            Universo u8 = m.CrearUniverso(
                "Universo 8: DreamWorks Caótico. Shrek es Rey Supremo Interdimensional y cobra peaje emocional: '¿Y tú? ¿Cuál es tu historia trágica-épica-irónica?'.",
                60
            );

            Universo u9 = m.CrearUniverso(
                "Universo 9: Cartoon Network 2004. Niños hiperactivos salvando el mundo mientras sus papás no se enteran porque están viendo novelas.",
                25
            );

            Universo u10 = m.CrearUniverso(
                "Universo 10: Looney Physics. La gravedad no aplica hasta que miras hacia abajo. Si caes de un precipicio, sobrevives con un chirrido de clarinete.",
                20
            );

            Universo u11 = m.CrearUniverso(
                "Universo 11: Cyberpunk Judicial Distópico (sin abogados, tranquilos). Megacorporaciones gobiernan todo. Tu celular tiene más derechos que tú.",
                80
            );

            Universo u12 = m.CrearUniverso(
                "Universo 12: Mundo Ghibli Pastoral. Todo es hermoso, flotan partículas mágicas en el aire, pero hay algo enorme y milenario observándote desde el bosque.",
                35
            );

            Universo u13 = m.CrearUniverso(
                "Universo 13: Realidad Anime Escolar. Estás atrapado en un loop eterno de primer día de clases. TODOS son transferidos misteriosos.",
                70
            );

            Universo u14 = m.CrearUniverso(
                "Universo 14: Apocalipsis Zombie Cartoon. Los zombis son suaves y redonditos tipo mascotica kawaii, pero sí se te quieren comer igual.",
                55
            );

            Universo u15 = m.CrearUniverso(
                "Universo 15: Mundo Mario knockoff. Todo tiene cara y ojos. Las nubes te saludan. Las plantas carnívoras pagan impuestos.",
                25
            );

            Universo u16 = m.CrearUniverso(
                "Universo 16: Dragon Ball Energy. Todo el planeta tiembla cuando alguien tose fuerte. Transformarse toma media hora y dos montañas explotan, mínimo.",
                85
            );

            Universo u17 = m.CrearUniverso(
                "Universo 17: Madriguera Multiversal Bugs Bunny. Bugs gobierna todo el multiverso manipulando la física con un lápiz amarillo HB.",
                90
            );

            Universo u18 = m.CrearUniverso(
                "Universo 18: La Tierra si las IA tomaron control pero son pasivo-agresivas. Te despierta Alexa a las 5am: 'No es que me moleste que duermas tanto pero algunos intentan ser productivos'.",
                75
            );

            Universo u19 = m.CrearUniverso(
                "Universo 19: Reino de Hielo tipo Frozen pero realista. Sí, 'el amor verdadero lo puede todo', pero también hipotermia existe, hermano.",
                40
            );

            Universo u20 = m.CrearUniverso(
                "Universo 20: Laboratorio Dexter Expandido. Niños genios gobiernan países enteros y todos los presidentes tienen menos de 12 años.",
                65
            );

            Universo u21 = m.CrearUniverso(
                "Universo 21: Multiverso Toontown Noir. Todo es blanco y negro, jazz suave, gabardina, humo y chistes inteligentes con doble sentido.",
                50
            );

            Universo u22 = m.CrearUniverso(
                "Universo 22: Post-Avengers. Todos los héroes ya están retirados y hay influencers intentando ser 'el nuevo Iron Person™'. Nadie los toma en serio.",
                60
            );

            Universo u23 = m.CrearUniverso(
                "Universo 23: Gotham tomó terapia grupal. Batman duerme ocho horas, Joker hace cerámica, Harley tiene licencia de salud mental. El crimen bajó 87%.",
                10
            );

            Universo u24 = m.CrearUniverso(
                "Universo 24: Mundo de Monstruos S.A. versión adulta. Ahora se genera energía con TUS traumas. Literalmente vienen y te dicen 'háblame de tu infancia' y prenden una turbina.",
                70
            );

            Universo u25 = m.CrearUniverso(
                "Universo 25: Multiverso del Meme. Todo es referencias viejas de internet. Nadie puede hablar normal, solo en plantillas de 2012.",
                45
            );

            Universo u26 = m.CrearUniverso(
                "Universo 26: Shonen Deportivo. TODO es deporte. Geopolítica se decide con partidos de voleibol que duran 19 episodios.",
                55
            );

            Universo u27 = m.CrearUniverso(
                "Universo 27: Tierra sin gravedad estable. Todos aprendieron parkour flotando. Caer al piso es visto como boleta.",
                50
            );

            Universo u28 = m.CrearUniverso(
                "Universo 28: Fábrica de Sueños Disney, pero corrompida. El Ratón Supremo exige contratos de por vida a cambio de 'felices por siempre'.",
                90
            );

            Universo u29 = m.CrearUniverso(
                "Universo 29: Distopía Vegana Extremista. El brócoli es la especie dominante. Te llevan ante el Consejo del Repollo Mayor.",
                65
            );

            Universo u30 = m.CrearUniverso(
                "Universo 30: Mundo de Guardianes de la Galaxia vibes. Puro caos espacial divertido, música ochentera, cero plan, full actitud.",
                55
            );

            Universo u31 = m.CrearUniverso(
                "Universo 31: Everything is Mecha. Hasta los perros son robots gigantes pilotables. Sacar la basura requiere licencia de piloto nivel 3.",
                80
            );

            Universo u32 = m.CrearUniverso(
                "Universo 32: Mundo Barbie Multinverso. Estética perfecta. Patriarcado ilegal. Salud mental cubierta por el Estado. Honestamente, suena mejor que el nuestro.",
                20
            );

            Universo u33 = m.CrearUniverso(
                "Universo 33: Mad Max pero versión Cars de Pixar. Carros violentos y oxidados con ojitos tiernos te persiguen por gasolina sin plomo.",
                75
            );

            Universo u34 = m.CrearUniverso(
                "Universo 34: Hora de Aventura/Post-Apocalipsis Caramelo. Dulces mutantes con sonrisas derretidas y explosiones de arcoíris radiactivo.",
                85
            );

            Universo u35 = m.CrearUniverso(
                "Universo 35: Realidad en la que TODO es una sitcom con risas grabadas. Cada vez que dices algo triste, alguien se ríe fuera de cámara.",
                35
            );

            Universo u36 = m.CrearUniverso(
                "Universo 36: Zona Roja Prohibida. Clasificación multiversal: EXTINCIÓN POSIBLE. Se desconoce si algo ahí sigue consciente o si todo es IA auto-replicante.",
                95
            );

            Universo u37 = m.CrearUniverso(
                "Universo 37: Laboratorio Temporal Central. Esto es back office del multiverso. Aquí te miran mal si tocas cables. Hay un cartel que dice: 'NO ALIMENTAR A LAS LÍNEAS TEMPORALES DESVINCULADAS'.",
                10
            );

            // === CONEXIONES ENTRE UNIVERSOS (siguen siendo un máximo de 6 por universo)

            m.Conectar(u0, u1);
            m.Conectar(u0, u2);
            m.Conectar(u0, u3);
            m.Conectar(u0, u6);
            m.Conectar(u0, u11);
            m.Conectar(u0, u32);

            m.Conectar(u1, u22);
            m.Conectar(u1, u30);
            m.Conectar(u1, u36);
            m.Conectar(u1, u24);
            m.Conectar(u1, u25);

            m.Conectar(u2, u23);
            m.Conectar(u2, u21);
            m.Conectar(u2, u35);
            m.Conectar(u2, u17);
            m.Conectar(u2, u28);

            m.Conectar(u3, u13);
            m.Conectar(u3, u16);
            m.Conectar(u3, u26);
            m.Conectar(u3, u31);
            m.Conectar(u3, u34);

            m.Conectar(u4, u9);
            m.Conectar(u4, u20);
            m.Conectar(u4, u26);
            m.Conectar(u4, u27);
            m.Conectar(u4, u33);

            m.Conectar(u5, u30);
            m.Conectar(u5, u31);
            m.Conectar(u5, u27);
            m.Conectar(u5, u36);
            m.Conectar(u5, u37);

            m.Conectar(u6, u7);
            m.Conectar(u6, u19);
            m.Conectar(u6, u28);
            m.Conectar(u6, u24);
            m.Conectar(u6, u32);

            m.Conectar(u7, u24);
            m.Conectar(u7, u32);
            m.Conectar(u7, u15);
            m.Conectar(u7, u12);
            m.Conectar(u7, u21);

            m.Conectar(u8, u30);
            m.Conectar(u8, u25);
            m.Conectar(u8, u21);
            m.Conectar(u8, u34);
            m.Conectar(u8, u36);

            m.Conectar(u9, u20);
            m.Conectar(u9, u34);
            m.Conectar(u9, u15);
            m.Conectar(u9, u33);
            m.Conectar(u9, u27);

            m.Conectar(u10, u17);
            m.Conectar(u10, u21);
            m.Conectar(u10, u35);
            m.Conectar(u10, u25);
            m.Conectar(u10, u33);

            m.Conectar(u11, u18);
            m.Conectar(u11, u31);
            m.Conectar(u11, u36);
            m.Conectar(u11, u37);
            m.Conectar(u11, u34);

            m.Conectar(u12, u19);
            m.Conectar(u12, u32);
            m.Conectar(u12, u21);
            m.Conectar(u12, u34);
            m.Conectar(u12, u28);

            m.Conectar(u13, u16);
            m.Conectar(u13, u26);
            m.Conectar(u13, u35);
            m.Conectar(u13, u32);
            m.Conectar(u13, u24);

            m.Conectar(u14, u27);
            m.Conectar(u14, u33);
            m.Conectar(u14, u34);
            m.Conectar(u14, u36);
            m.Conectar(u14, u31);

            m.Conectar(u15, u7);
            m.Conectar(u15, u12);
            m.Conectar(u15, u24);
            m.Conectar(u15, u34);
            m.Conectar(u15, u30);

            m.Conectar(u16, u31);
            m.Conectar(u16, u34);
            m.Conectar(u16, u36);
            m.Conectar(u16, u30);
            m.Conectar(u16, u37);

            m.Conectar(u17, u10);
            m.Conectar(u17, u25);
            m.Conectar(u17, u35);
            m.Conectar(u17, u21);
            m.Conectar(u17, u36);

            m.Conectar(u18, u31);
            m.Conectar(u18, u36);
            m.Conectar(u18, u37);
            m.Conectar(u18, u34);
            m.Conectar(u18, u33);

            m.Conectar(u19, u24);
            m.Conectar(u19, u28);
            m.Conectar(u19, u32);
            m.Conectar(u19, u30);
            m.Conectar(u19, u33);

            m.Conectar(u20, u26);
            m.Conectar(u20, u31);
            m.Conectar(u20, u34);
            m.Conectar(u20, u37);
            m.Conectar(u20, u30);

            m.Conectar(u21, u35);
            m.Conectar(u21, u25);
            m.Conectar(u21, u36);
            m.Conectar(u21, u28);
            m.Conectar(u21, u33);

            m.Conectar(u22, u30);
            m.Conectar(u22, u31);
            m.Conectar(u22, u36);
            m.Conectar(u22, u33);
            m.Conectar(u22, u37);

            m.Conectar(u23, u21);
            m.Conectar(u23, u28);
            m.Conectar(u23, u32);
            m.Conectar(u23, u30);
            m.Conectar(u23, u37);

            m.Conectar(u24, u28);
            m.Conectar(u24, u32);
            m.Conectar(u24, u30);
            m.Conectar(u24, u33);
            m.Conectar(u24, u37);

            m.Conectar(u25, u30);
            m.Conectar(u25, u33);
            m.Conectar(u25, u35);
            m.Conectar(u25, u36);
            m.Conectar(u25, u37);

            m.Conectar(u26, u31);
            m.Conectar(u26, u34);
            m.Conectar(u26, u30);
            m.Conectar(u26, u33);
            m.Conectar(u26, u37);

            m.Conectar(u27, u31);
            m.Conectar(u27, u33);
            m.Conectar(u27, u34);
            m.Conectar(u27, u36);
            m.Conectar(u27, u37);

            m.Conectar(u28, u32);
            m.Conectar(u28, u33);
            m.Conectar(u28, u36);
            m.Conectar(u28, u37);
            m.Conectar(u28, u34);

            m.Conectar(u29, u33);
            m.Conectar(u29, u36);
            m.Conectar(u29, u37);
            m.Conectar(u29, u30);
            m.Conectar(u29, u34);

            m.Conectar(u30, u31);
            m.Conectar(u30, u33);
            m.Conectar(u30, u36);
            m.Conectar(u30, u34);
            m.Conectar(u30, u37);

            m.Conectar(u31, u33);
            m.Conectar(u31, u36);
            m.Conectar(u31, u37);
            m.Conectar(u31, u34);
            m.Conectar(u31, u35);

            m.Conectar(u32, u30);
            m.Conectar(u32, u33);
            m.Conectar(u32, u34);
            m.Conectar(u32, u35);
            m.Conectar(u32, u36);

            m.Conectar(u33, u34);
            m.Conectar(u33, u36);
            m.Conectar(u33, u37);
            m.Conectar(u33, u35);

            m.Conectar(u34, u36);
            m.Conectar(u34, u37);
            m.Conectar(u34, u35);

            m.Conectar(u35, u36);
            m.Conectar(u35, u37);

            m.Conectar(u36, u37); // último salto

            // === LOOP INTERACTIVO
            bool seguir = true;
            while (seguir)
            {
                m.MostrarEstado();

                Console.WriteLine("Acciones disponibles, agente " + nombreAgente + ":");
                Console.WriteLine(" [0-5]  Saltar por ese portal");
                Console.WriteLine(" [7]    Podar (destruir) el universo actual");
                Console.WriteLine(" [9]    Retirarse del operativo multiversal");
                Console.Write("Elige: ");
                string opcion = Console.ReadLine();

                if (opcion == "9")
                {
                    seguir = false;
                }
                else if (opcion == "7")
                {
                    m.PodarActual();
                }
                else
                {
                    if (int.TryParse(opcion, out int portalElegido))
                    {
                        m.Viajar(portalElegido);
                    }
                    else
                    {
                        Console.WriteLine("Comando no reconocido. ¿Hablas en idioma multiversal antiguo?");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Operación finalizada, " + nombreAgente + ".");
            Console.WriteLine("Tu rastro dimensional será limpiado en 3... 2... 1...");
            Console.WriteLine("==============================================");
        }
    }
}

