using UnityEngine;

public static class ProjectileLauncher  
{
    public static Vector3 CalculateLaunchForce(Vector3 startPosition, Vector3 targetPosition, float launchAngle)
    {
        // Distância entre os dois pontos
        Vector3 direction = targetPosition - startPosition;
        float distance = direction.magnitude;

        // Altura entre os dois pontos
        float heightDifference = targetPosition.y - startPosition.y;

        // Ângulo de lançamento em radianos
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // Gravidade padrão da Unity
        float gravity = Physics.gravity.y;

        // Fórmula para calcular a velocidade inicial necessária
        float initialVelocity = Mathf.Sqrt(distance * Mathf.Abs(gravity) / Mathf.Sin(2 * angleInRadians));

        // Calcula a componente horizontal e vertical da velocidade inicial
        float velocityX = initialVelocity * Mathf.Cos(angleInRadians);
        float velocityY = initialVelocity * Mathf.Sin(angleInRadians);

        // Direção horizontal do lançamento
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z).normalized;

        // Vetor de força resultante
        Vector3 launchForce = horizontalDirection * velocityX;
        launchForce.y = velocityY;

        return launchForce;
    }
}
