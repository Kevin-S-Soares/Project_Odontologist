import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      userId: requestBody["userId"],
      hash: requestBody["hash"],
      operation: requestBody["operation"],
      password: requestBody["password"],
    }),
    method: "POST",
  };
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/reset_password`,
    options,
  );
  return new Response(JSON.stringify(body.ok), {
    status: body.status,
  });
};
