# Business

Proyecto con la lógica de negocio de la aplicación ImplantDent

| Sonarqube |
|---|
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_business&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Implantdent_business) |
| [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_business&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Implantdent_business) |
| [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_business&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Implantdent_business) |
| [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_business&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Implantdent_business) |

## CI/CD

Se ejecuta el pipeline https://github.com/Implantdent/business/actions/workflows/build.yml

| Rama | Estado |
|:-:|:-:|
| dev | [![Compilar](https://github.com/Implantdent/business/actions/workflows/build.yml/badge.svg?branch=dev)](https://github.com/Implantdent/business/actions/workflows/build.yml) |
| qa | [![Compilar](https://github.com/Implantdent/business/actions/workflows/build.yml/badge.svg?branch=qa)](https://github.com/Implantdent/business/actions/workflows/build.yml) |
| main | [![Compilar](https://github.com/Implantdent/business/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/Implantdent/business/actions/workflows/build.yml) |

El despliegue se ejecuta en

| Rama | NuGet |
|:-:|:-:|
| dev | Business 1.0.X-dev |
| qa | Business 1.0.X-qa |
| main | Business 1.0.X |

## Lenguaje

C# .Net 8

## Librerías y paquetes

| Paquete | Versión |
|:-:|:-:|
| xUnit | 2.5.3 |
| Dapper | 2.1.35 |
| Dal | 1.0.1 |

## Compilar y probar

Se ejecuta el proyecto de pruebas Business.Test